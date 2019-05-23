using IronShop.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.EntityConfigurations
{
    public class PermissionMenuItemConfiguration : IEntityTypeConfiguration<PermissionMenuItem>
    {
        public void Configure(EntityTypeBuilder<PermissionMenuItem> builder)
        {
            builder
                .HasKey(p => p.PermissionMenuItemId);

            builder
                .Property(p => p.PermissionMenuItemId)
                .IsRequired();

            builder
               .Property(p => p.DisplayName)
               .HasMaxLength(100);

            builder
               .Property(p => p.Display)
               .IsRequired();

            builder
               .Property(p => p.Url)
               .IsRequired()
               .HasMaxLength(50);




        }
    }
}
