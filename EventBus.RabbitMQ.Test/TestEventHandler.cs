using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ.Test
{
    public class TestEventHandler : IEventHandler<TestEvent>
    {
        public async Task HandleAsync(TestEvent @event)
        {
        }
    }
}
