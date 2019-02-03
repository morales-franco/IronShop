using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos
{
    public class ProductDto
    {
        public int? ProductId { get; set; }

        [Required, MaxLength(100)]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }
    }
}
