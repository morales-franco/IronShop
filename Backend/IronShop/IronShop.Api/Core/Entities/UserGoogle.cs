using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class UserGoogle
    {
        public string TokenGoogle { get; private set; }

        public UserGoogle(string tokenGoogle)
        {
            TokenGoogle = tokenGoogle;
        }
    }
}
