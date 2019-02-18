using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities.Base
{
    public class IronFile
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public bool Success { get;  set; }

        public byte[] Metadata { get; set; }
        public string ContentType { get; set; }

        public IronFile()
        {
            Success = false;
        }
    }
}
