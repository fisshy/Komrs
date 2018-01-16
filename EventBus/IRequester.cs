using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IRequester
    {
        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request);
    }
}
