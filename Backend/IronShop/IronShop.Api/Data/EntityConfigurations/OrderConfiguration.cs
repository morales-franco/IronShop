using IronShop.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
               .HasKey(p => p.OrderId);

            builder
               .Property(p => p.OrderId)
               .IsRequired();

            builder
               .Property(p => p.OrderDate)
               .IsRequired();

            builder
               .Property(p => p.OrderNumber)
               .IsRequired();

            builder
                .HasIndex(p => p.OrderNumber)
                .IsUnique();

            builder
               .Property(p => p.UserId)
               .IsRequired();


        }
    }
}
