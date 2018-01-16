using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.RabbitMQ.Test
{
    public class TestEvent : IEvent
    {
        public string Test { get; set; }
    }
}
