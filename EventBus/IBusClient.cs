using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IBusClient : IPublisher, ISubscriber, IRequester
    {
        
    }
}
