using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PreProcessorTest.Messages;
using PreProcessorTest.Messages.Commands;

namespace PreProcessorTest.Pipeline
{
    public class MyCommandPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("***** MYCOMMAND PREPROCESSOR CALLED *****");

            return Task.CompletedTask;
        }
    }
}
