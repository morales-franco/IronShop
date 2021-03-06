﻿// <auto-generated />
using System;
using IronShop.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IronShop.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190313142659_add-sp-indexPagedProduct")]
    partial class addspindexPagedProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IronShop.Api.Core.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("OrderDate");

                    b.Property<long>("OrderNumber");

                    b.Property<int>("UserId");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderNumber")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("IronShop.Api.Core.Entities.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("UnitPrice");

                    b.Property<int>("Units");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("IronShop.Api.Core.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AuditDate");

                    b.Property<string>("AuditUserName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<string>("ImageFileName")
                        .HasMaxLength(256);

                    b.Property<decimal>("Price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("IronShop.Api.Core.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<bool>("GoogleAuth")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("ImageFileName")
                        .HasMaxLength(256);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("IronShop.Api.Core.Entities.Order", b =>
                {
                    b.HasOne("IronShop.Api.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IronShop.Api.Core.Entities.OrderItem", b =>
                {
                    b.HasOne("IronShop.Api.Core.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("IronShop.Api.Core.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
