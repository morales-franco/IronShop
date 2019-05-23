using IronShop.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.EntityConfigurations
{
    public class RolePermissionMenuItemConfiguration : IEntityTypeConfiguration<RolePermissionMenuItem>
    {
        public void Configure(EntityTypeBuilder<RolePermissionMenuItem> builder)
        {
            builder
                .HasKey(rp => new { rp.RoleId, rp.PermissionMenuItemId });

            builder
                .HasOne(rp => rp.Role)
                .WithMany(p => p.RolePermissionMenuItem)
                .HasForeignKey(rp => rp.RoleId);

            builder
                .HasOne(rp => rp.PermissionMenuItem)
                .WithMany(p => p.RolePermissionMenuItem)
                .HasForeignKey(rp => rp.PermissionMenuItemId);
        }
    }

}
