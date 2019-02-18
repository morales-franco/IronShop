using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class UserGoogle
    {
        public string Email { get; private set; }
        public string TokenGoogle { get; private set; }

        public UserGoogle(string email, string tokenGoogle)
        {
            Email = email;
            TokenGoogle = tokenGoogle;
        }
    }
}
