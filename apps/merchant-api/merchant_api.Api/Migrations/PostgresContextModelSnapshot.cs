﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using merchant_api.Data.Data;

#nullable disable

namespace merchant_api.Api.Migrations
{
    [DbContext(typeof(PostgresContext))]
    partial class PostgresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uuid");

                    b.HasKey("CategoriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CategoryProduct");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AltText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Image", (string)null);
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("BasePrice")
                        .HasColumnType("double precision");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("LowStockThreshold")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.ProductAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProductVariantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("VariantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProductVariantId");

                    b.HasIndex("VariantId");

                    b.ToTable("ProductAttribute", (string)null);
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.ProductVariant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<double>("PriceAdjustment")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ImageId")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVariant", (string)null);
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Variant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Variant", (string)null);
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.WishList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("WishLists", (string)null);
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("merchant_api.Data.Models.Concretes.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("merchant_api.Data.Models.Concretes.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Category", b =>
                {
                    b.HasOne("merchant_api.Data.Models.Concretes.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Image", b =>
                {
                    b.HasOne("merchant_api.Data.Models.Concretes.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.ProductAttribute", b =>
                {
                    b.HasOne("merchant_api.Data.Models.Concretes.ProductVariant", "ProductVariant")
                        .WithMany("Attributes")
                        .HasForeignKey("ProductVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("merchant_api.Data.Models.Concretes.Variant", "Variant")
                        .WithMany("ProductAttributes")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductVariant");

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.ProductVariant", b =>
                {
                    b.HasOne("merchant_api.Data.Models.Concretes.Image", "Image")
                        .WithOne("ProductVariant")
                        .HasForeignKey("merchant_api.Data.Models.Concretes.ProductVariant", "ImageId");

                    b.HasOne("merchant_api.Data.Models.Concretes.Product", "Product")
                        .WithMany("ProductVariants")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.WishList", b =>
                {
                    b.HasOne("merchant_api.Data.Models.Concretes.Product", "Product")
                        .WithMany("WishList")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Image", b =>
                {
                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("ProductVariants");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.ProductVariant", b =>
                {
                    b.Navigation("Attributes");
                });

            modelBuilder.Entity("merchant_api.Data.Models.Concretes.Variant", b =>
                {
                    b.Navigation("ProductAttributes");
                });
#pragma warning restore 612, 618
        }
    }
}
