using PreProcessorTest.Messages;
using PreProcessorTest.Messages.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Threading;

namespace PreProcessorTest.Handlers
{
    public class MyQueryHandler : ICommandHandler<MyQuery, CommonResponse>
    {
        public async Task<CommonResponse> Handle(MyQuery request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("   ***** Query handler executing *****");

            return
                new CommonResponse(
                    succeeded: true,
                    data: "Query execution completed successfully.");
        }
    }
}
