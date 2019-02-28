using IronShop.Api.Core.Entities.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos.Index
{
    public class UserIndexDto: IPageEntity
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ImageFileName { get; set; }
        public bool GoogleAuth { get; set; }


        public int TotalRows { get; set; }
    }
}
