using IronShop.Api.Core.Dtos;
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
        public ProfileDto Profile { get; set; }

        public IronToken(string token, DateTime expiration, ProfileDto profile)
        {
            Token = token;
            Expiration = expiration;
            Profile = profile;
        }
    }
}
