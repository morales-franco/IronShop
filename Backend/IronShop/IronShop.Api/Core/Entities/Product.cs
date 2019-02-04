using Ardalis.GuardClauses;
using IronShop.Api.Core.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities
{
    public class Product: IAuditable
    {
        public int ProductId { get; private set; }
        public string Category { get; private set; }
        public decimal Price { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string AuditUserName { get; set; }
        public DateTime AuditDate { get;  set; }
        public bool Deleted { get; private set; }

        public Product()
        {

        }

        public Product(int? productId, string category, decimal price, string title, string description)
        {
            Guard.Against.NullOrEmpty(category, nameof(category));
            Guard.Against.NullOrEmpty(description, nameof(description));
            Guard.Against.NullOrEmpty(title, nameof(title));
            Guard.Against.UpperZero(price);

            ProductId = productId ?? 0;
            Category = category;
            Price = price;
            Title = title;
            Description = description;
            Deleted = false;
        }

        public void MarkAsDeleted()
        {
            Deleted = true;
        }

    }
}
