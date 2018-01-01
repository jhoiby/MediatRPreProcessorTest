using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.OData;

namespace PreProcessorTest.Messages.Commands
{
    public interface ICommand<TResponse> : IMessage<TResponse>
    {
    }
}
