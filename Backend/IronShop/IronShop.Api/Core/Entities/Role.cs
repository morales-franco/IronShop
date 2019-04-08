using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Description { get; set; }
        public List<RolePermission> RolePermission { get; set; }
        public List<User> Users { get; set; }
    }
}
