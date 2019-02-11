using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos.Index
{
    public class UserIndexDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
