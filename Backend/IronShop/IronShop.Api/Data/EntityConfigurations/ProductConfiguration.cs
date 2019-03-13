using IronShop.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(p => p.ProductId);

            builder
                .Property(p => p.ProductId)
                .IsRequired();

            builder
                .Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);

            builder
               .Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(512);

            builder
               .Property(p => p.Price)
               .IsRequired();

            builder
               .Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(100);

            builder
               .Property(p => p.AuditUserName)
               .IsRequired()
               .HasMaxLength(256);

            builder
               .Property(p => p.AuditDate)
               .IsRequired();

            builder
               .Property(u => u.ImageFileName)
               .HasMaxLength(256);

        }
    }
}
