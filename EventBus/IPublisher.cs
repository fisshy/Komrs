using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IPublisher
    {
        void PublishEvent<T>(T @event) where T : IEvent;
        void PublishCommand<T>(T command) where T : ICommand;
    }
}
