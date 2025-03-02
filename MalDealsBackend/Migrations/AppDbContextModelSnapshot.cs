﻿// <auto-generated />
using System;
using MalDealsBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MalDealsBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MalDealsBackend.Models.Entitys.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP WITH TIME ZONE")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean")
                        .HasColumnName("is_public");

                    b.PrimitiveCollection<string[]>("SubCategoryIds")
                        .HasColumnType("text[]")
                        .HasColumnName("sub_category_ids");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("text")
                        .HasColumnName("thumbnail");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TIMESTAMP WITH TIME ZONE")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("categories");
                });

            modelBuilder.Entity("MalDealsBackend.Models.Entitys.DealEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.PrimitiveCollection<string[]>("Categories")
                        .HasColumnType("text[]")
                        .HasColumnName("categories");

                    b.Property<string>("CouponCode")
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("coupon_code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP WITH TIME ZONE")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ExpirationDate")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("expiration_date");

                    b.PrimitiveCollection<string[]>("ImagesUrl")
                        .HasColumnType("text[]")
                        .HasColumnName("images_url");

                    b.Property<bool>("IsFree")
                        .HasColumnType("boolean")
                        .HasColumnName("is_free");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("boolean")
                        .HasColumnName("is_publish");

                    b.Property<decimal?>("OfferPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("offer_price");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<string>("Provider")
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("provider");

                    b.Property<string>("ProviderUrl")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("provider_url");

                    b.Property<decimal>("ShipingCost")
                        .HasColumnType("numeric")
                        .HasColumnName("shipping_cost");

                    b.PrimitiveCollection<string[]>("Tags")
                        .HasColumnType("text[]")
                        .HasColumnName("tags");

                    b.Property<string>("ThumbnailUrl")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("thumbnail_url");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TIMESTAMP WITH TIME ZONE")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserId")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("user_id");

                    b.Property<string>("VideoUrl")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("video_url");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("deals");
                });

            modelBuilder.Entity("MalDealsBackend.Models.Entitys.DealVoteEntity", b =>
                {
                    b.Property<Guid>("DealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("deal_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<int>("Value")
                        .HasColumnType("integer")
                        .HasColumnName("value");

                    b.HasKey("DealId");

                    b.ToTable("deal_vote");
                });

            modelBuilder.Entity("MalDealsBackend.Models.Entitys.ProviderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("text")
                        .HasColumnName("logo_url");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("providers");
                });

            modelBuilder.Entity("MalDealsBackend.Models.Entitys.SwaggerUserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("swagger_users");
                });

            modelBuilder.Entity("MalDealsBackend.Models.Entitys.TagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("tags");
                });

            modelBuilder.Entity("MalDealsBackend.Models.Entitys.UserDataEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("text")
                        .HasColumnName("photo_url");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<bool>("Verified")
                        .HasColumnType("boolean")
                        .HasColumnName("verified");

                    b.HasKey("UserId");

                    b.ToTable("user_data");
                });

            modelBuilder.Entity("MalDealsBackend.Models.Entitys.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<bool>("Verified")
                        .HasColumnType("boolean")
                        .HasColumnName("verified");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("MalDealsBackend.Models.Entitys.UserMarketDealEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("DealId")
                        .HasColumnType("uuid")
                        .HasColumnName("deal_id");

                    b.HasKey("UserId");

                    b.ToTable("user_market_deals");
                });
#pragma warning restore 612, 618
        }
    }
}
