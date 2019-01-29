using IronShop.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.UserId);

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .Property(u => u.UserId)
                .IsRequired();

            builder
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(512);

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder
               .Property(u => u.Role)
               .IsRequired()
               .HasMaxLength(50);

            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
