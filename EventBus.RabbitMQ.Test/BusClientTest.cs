using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EventBus.RabbitMQ.Test
{
    public class BusClientTest
    {
        [Fact]
        public async Task ShouldProcessMessage()
        {
            var bus = new BusClient(null, null);
            bus.SubscribeToEvent<TestEvent, TestEventHandler>();
            await bus.ProcessMessage(typeof(TestEvent).Name, JsonConvert.SerializeObject(new TestEvent { Test = "Hello World" }));
        }
    }
}
