using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreProcessorTest.Messages
{
    public class CommonResponse : ICommonResponse
    {
        private readonly bool _succeeded;
        private readonly string _data;

        public CommonResponse(bool succeeded, string data = default(string))
        {
            _succeeded = succeeded;
            _data = data;
        }

        public bool Succeeded => _succeeded;

        public string Data => _data;
    }
}
