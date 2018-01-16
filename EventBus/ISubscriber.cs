using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus
{
    public interface ISubscriber
    {
        void SubscribeToCommand<TCommand, TCommandHandler>() 
            where TCommand : ICommand 
            where TCommandHandler : ICommandHandler<TCommand>;

        void SubscribeToEvent<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : IEventHandler<TEvent>;
    }
}
