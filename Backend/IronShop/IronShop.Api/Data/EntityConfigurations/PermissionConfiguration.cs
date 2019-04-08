using IronShop.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.EntityConfigurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder
                .HasKey(p => p.PermissionId);

            builder
                .Property(p => p.PermissionId)
                .IsRequired();

            builder
               .Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(100);

            builder
               .Property(p => p.Icon)
               .IsRequired()
               .HasMaxLength(50);

            builder
               .Property(p => p.Url)
               .IsRequired()
               .HasMaxLength(50);


        }
    }
}
