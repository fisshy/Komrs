using EventBus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Komrs.User.Events
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        public async Task HandleAsync(UserCreated @event)
        {
            Trace.Write($"User created {@event.Id} {@event.UserName} {@event.Email}");
        }
    }
}
