using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos
{
    public class OrderItemDto
    {
        public int? OrderItemId { get;  set; }
        [Required]
        public int ProductId { get;  set; }
        [Required]
        public string ProductTitle { get;  set; }
        [Required]
        public string ProductCategory { get; set; }
        [Required]
        public int Units { get;  set; }
        [Required]
        public decimal UnitPrice { get;  set; }
    }
}
