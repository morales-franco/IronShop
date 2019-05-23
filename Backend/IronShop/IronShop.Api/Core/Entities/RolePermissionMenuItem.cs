using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class RolePermissionMenuItem
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionMenuItemId { get; set; }
        public PermissionMenuItem PermissionMenuItem { get; set; }
    }
}
