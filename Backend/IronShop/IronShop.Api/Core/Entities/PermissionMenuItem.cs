using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class PermissionMenuItem
    {
        public int PermissionMenuItemId { get; private set; }
        public string DisplayName { get; set; }
        public bool Display { get; private set; }
        public string Url { get; private set; }
        public int? Order { get; private set; }
        public int? MenuId { get; private set; }
        public Menu Menu { get; private set; }
        public List<RolePermissionMenuItem> RolePermissionMenuItem { get; set; }

    }
}
