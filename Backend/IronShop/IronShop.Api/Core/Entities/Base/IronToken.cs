using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities.Base
{
    public class IronToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public IronToken(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}
