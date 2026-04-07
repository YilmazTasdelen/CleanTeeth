using CleanTeeth.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Application.Utilities
{
    public class SimpleMediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        public SimpleMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());
            var validator = _serviceProvider.GetService(validatorType);

            if (validator is not null)
            {
                var validateMethod = validatorType.GetMethod("ValidateAsync");
                var taskToValidate = (Task)validateMethod!.Invoke(validator, new object[] { request, CancellationToken.None })!;

                await taskToValidate;

                var result = taskToValidate.GetType().GetProperty("Result");
                var validationResult = (ValidationResult)result!.GetValue(taskToValidate)!;

                if (!validationResult.IsValid)
                {
                    throw new CustomValidationException(validationResult);
                }
            }

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new MediatorException($"No handler found for request of type {request.GetType()}");
            }

            var method = handlerType.GetMethod("Handle")!; 

            return await (Task<TResponse>)method.Invoke(handler, new object[] { request })!;
        }
    }
}
