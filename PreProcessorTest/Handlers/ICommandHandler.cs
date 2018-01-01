using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace PreProcessorTest.Handlers
{
    public interface ICommandHandler<in TRequest, TResponse> : IMessageHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
}
