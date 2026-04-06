using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Application.Utilities
{
    public class SimplMediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        public SimplMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException($"No handler found for request of type {request.GetType()}");
            }

            var method = handlerType.GetMethod("Handle")!; 

            return await (Task<TResponse>)method.Invoke(handler, new object[] { request })!;
        }
    }
}
