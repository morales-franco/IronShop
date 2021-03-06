﻿using Ardalis.GuardClauses;
using IronShop.Api.Core.Entities.Base;
using System;

namespace IronShop.Api.Core.Entities
{
    public class Product: IAuditable
    {
        public int ProductId { get; private set; }
        public string Category { get; private set; }
        public decimal Price { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageFileName { get; private set; }

        public string AuditUserName { get; set; }
        public DateTime AuditDate { get;  set; }
        public bool Deleted { get; private set; }

        public Product()
        {

        }

        public Product(int? productId, string category, decimal price, string title, string description, string imageFileName = null)
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
            ImageFileName = imageFileName;
        }

        public void MarkAsDeleted()
        {
            Deleted = true;
        }

        public void SetImage(string fileName)
        {
            ImageFileName = fileName;
        }
    }
}
