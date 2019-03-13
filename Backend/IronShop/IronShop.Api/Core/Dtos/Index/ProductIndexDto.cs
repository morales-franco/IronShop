using IronShop.Api.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos.Index
{
    public class ProductIndexDto : IPageEntity
    {
        public int ProductId { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }

        public string ImageFileName { get; set; }

        public int TotalRows { get; set; }
    }
}
