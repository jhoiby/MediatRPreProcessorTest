﻿using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PreProcessorTest.Pipeline
{
    public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("***** GENERIC PREPROCESSOR CALLED *****");
            Debug.WriteLine("     * Type of request: " + typeof(TRequest));

            return Task.CompletedTask;
        }
    }
}
