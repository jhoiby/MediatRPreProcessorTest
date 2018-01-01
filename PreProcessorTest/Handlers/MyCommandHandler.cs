using PreProcessorTest.Messages;
using PreProcessorTest.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PreProcessorTest.Handlers
{
    public class MyCommandHandler : ICommandHandler<MyCommand, CommonResponse>
    {
        public async Task<CommonResponse> Handle(MyCommand request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("   ***** Command handler executing *****");

            return
                new CommonResponse(
                    succeeded: true,
                    data: "Command execution completed successfully.");
        }
    }
}
