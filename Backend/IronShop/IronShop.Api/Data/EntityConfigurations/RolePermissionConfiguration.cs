using IronShop.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.EntityConfigurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder
                .HasOne(rp => rp.Role)
                .WithMany(p => p.RolePermission)
                .HasForeignKey(rp => rp.RoleId);

            builder
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermission)
                .HasForeignKey(rp => rp.PermissionId);
        }
    }

}
