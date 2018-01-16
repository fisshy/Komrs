using EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.User.Events
{
    public class UserCreated : IEvent
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
