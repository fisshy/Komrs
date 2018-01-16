using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Polly.Retry;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using Polly;
using System.Reflection;

namespace EventBus.RabbitMQ
{
    public class BusClient : IBusClient
    {
        const string BROKER_NAME = "komrs_event_bus";

        private readonly IPersistentConnection _persistentConnection;
        private readonly ILogger<BusClient> _logger;
        private readonly string _queueName;
        private readonly int _retryCount;
        private IModel _consumerChannel;

        private Dictionary<Type, HashSet<Type>> EventSubscriptions { get; set; }
        private Dictionary<Type, HashSet<Type>> CommandSubscription { get; set; }

        public BusClient(IPersistentConnection persistentConnection, ILogger<BusClient> logger,
            string queueName = null, int retryCount = 5)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
            _queueName = queueName;
            _retryCount = retryCount;
            _consumerChannel = CreateConsumerChannel();

            EventSubscriptions = new Dictionary<Type, HashSet<Type>>();
            CommandSubscription = new Dictionary<Type, HashSet<Type>>();
        }

        private void EnsureConnection()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
        }

        public void PublishEvent<T>(T @event) where T : IEvent
        {
            EnsureConnection();
            Publish(@event);
        }

        public void PublishCommand<T>(T command) where T : ICommand
        {
            EnsureConnection();
            Publish(command);
        }

        private void Publish<T>(T @event)
        {
            var policy = Policy.Handle<BrokerUnreachableException>()
                            .Or<SocketException>()
                            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                            {
                                _logger.LogWarning(ex.ToString());
                            });

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    channel.BasicPublish(exchange: BROKER_NAME,
                                     routingKey: eventName,
                                     basicProperties: null,
                                     body: body);
                });
            }
        }

        public Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
        {
            throw new NotImplementedException();
        }

        public void SubscribeToCommand<TCommand, TCommandHandler>()
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>
        {
            EnsureConnection();

            var type = typeof(TCommand);
            var handler = typeof(TCommandHandler);

            if (!CommandSubscriptionExists(type))
            {
                CommandSubscription.Add(type, new HashSet<Type>() { handler });
                return;
            }

            CommandSubscription[type].Add(handler);

            Subscribe(type.Name);
        }

        public void SubscribeToEvent<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : IEventHandler<TEvent>
        {

            var type = typeof(TEvent);
            var handler = typeof(TEventHandler);

            if (!EventSubscriptionExists(type))
            {
                EventSubscriptions.Add(type, new HashSet<Type>() { handler });
                Subscribe(type.Name);
                return;
            }

            if (!EventSubscriptions[type].Contains(handler))
            {
                EventSubscriptions[type].Add(handler);
            }

            Subscribe(type.Name);
        }

        private void Subscribe(string eventName)
        {
            if (_persistentConnection == null)
            {
                return;
            }

            EnsureConnection();

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueBind(queue: _queueName,
                                  exchange: BROKER_NAME,
                                  routingKey: eventName);
            }
        }

        private string GetEventName<TEvent>()
        {
            return typeof(TEvent).Name;
        }

        private IModel CreateConsumerChannel()
        {
            if (_persistentConnection == null)
            {
                return null;
            }

            EnsureConnection();

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME,
                                 type: "direct");

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);

                await ProcessMessage(eventName, message);

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: _queueName,
                                 autoAck: false,
                                 consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        public async Task ProcessMessage(string eventName, string message)
        {
            await ProcessEvents(eventName, message);
            await ProcessCommands(eventName, message);
        }

        private async Task ProcessCommands(string eventName, string message)
        {
            await Process(eventName, message, CommandSubscription);
        }

        private async Task ProcessEvents(string eventName, string message)
        {
            await Process(eventName, message, EventSubscriptions);
        }

        private async Task Process(string eventName, string message, Dictionary<Type, HashSet<Type>> subs)
        {
            var subType = FindType(eventName, subs);

            if (subType == null)
            {
                return;
            }

            var handlers = subs[subType];

            foreach (var handler in handlers)
            {
                try
                {
                    var deserializedMessage = JsonConvert.DeserializeObject(message, subType);
                    ConstructorInfo ctor = handler.GetConstructor(Type.EmptyTypes);
                    object hndlr = ctor.Invoke(new object[] { });
                    MethodInfo mtd = handler.GetMethod("HandleAsync");
                    await (Task)mtd.Invoke(hndlr, new object[] { deserializedMessage });
                }
                catch(Exception ex)
                {
                    _logger.LogCritical(ex, $"Unable to process ${eventName}");
                }
            }
        }

        private bool EventSubscriptionExists(Type eventType)
        {
            return EventSubscriptions != null && EventSubscriptions.ContainsKey(eventType);
        }

        private bool CommandSubscriptionExists(Type eventType)
        {
            return CommandSubscription != null && CommandSubscription.ContainsKey(eventType);
        }

        private Type FindType(string eventName, Dictionary<Type, HashSet<Type>> subs)
        {
            if (subs == null)
            {
                return null;
            }

            foreach (var sub in subs)
            {
                var name = sub.Key.Name;
                if (name == eventName)
                {
                    return sub.Key;
                }
            }

            return null;
        }

        private Type FindCommandType(string eventName)
        {
            if (CommandSubscription == null)
            {
                return null;
            }

            foreach (var sub in CommandSubscription)
            {
                var name = sub.Key.Name;
                if (name == eventName)
                {
                    return sub.Key;
                }
            }

            return null;
        }
    }
}
