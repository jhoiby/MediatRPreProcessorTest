using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreProcessorTest.Messages
{
    public interface ICommonResponse
    {
        bool Succeeded { get; }
        string Data { get; }
    }
}
