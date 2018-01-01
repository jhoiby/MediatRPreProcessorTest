using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreProcessorTest.Messages.Queries
{
    public interface IQuery<TResponse> : IMessage<TResponse>
    {
    }
}
