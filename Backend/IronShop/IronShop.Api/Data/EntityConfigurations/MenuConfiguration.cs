using IronShop.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.EntityConfigurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder
                .HasKey(p => p.MenuId);

            builder
                .Property(p => p.MenuId)
                .IsRequired()
                .ValueGeneratedNever();

            builder
               .Property(p => p.Icon)
               .IsRequired()
               .HasMaxLength(50);

            builder
              .Property(p => p.DisplayName)
              .HasMaxLength(100);
        }
    }
}
