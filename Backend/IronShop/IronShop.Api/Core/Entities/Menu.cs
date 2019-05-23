using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class Menu
    {
        public int MenuId { get; private set; }
        public string DisplayName { get; private set; }
        public string Icon { get; private set; }
        public List<PermissionMenuItem> PermissionMenuItem { get; set; }
    }
}
