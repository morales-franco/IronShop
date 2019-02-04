using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos
{
    public class OrderDto
    {
        public int? OrderId { get;  set; }
        public DateTime OrderDate { get;  set; }
        public long? OrderNumber { get;  set; }
        public IList<OrderItemDto> Items { get;  set; }
        public int? UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
