using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos
{
    public class OrderItemDto
    {
        public int? OrderItemId { get;  set; }
        public int ProductId { get;  set; }
        public string ProductTitle { get;  set; }
        public string ProductCategory { get; set; }
        public int Units { get;  set; }
        public decimal UnitPrice { get;  set; }
    }
}
