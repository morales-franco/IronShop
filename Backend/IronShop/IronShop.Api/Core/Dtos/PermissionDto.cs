using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos
{
    public class PermissionDto
    {
        public int PermissionId { get; set; }
        public string DisplayName { get; set; }
        public bool Display { get; set; }
        public string Url { get; set; }
        public int? MenuId { get; set; }
        public string MenuDisplayName { get; set; }
        public string MenuIcon { get; set; }
    }
}
