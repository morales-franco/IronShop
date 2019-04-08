using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class Permission
    {
        public int PermissionId { get; private set; }
        public string Title { get; private set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        private int? ParentPermissionId { get; set; }


        public Permission Parent { get; set; }
        public List<Permission> SubPermissions { get; set; }
        public List<RolePermission> RolePermission { get; set; }
    }
}
