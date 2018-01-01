using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace PreProcessorTest.Messages
{
    public interface IMessage<TResponse> : IRequest<TResponse>
    {
    }
}
