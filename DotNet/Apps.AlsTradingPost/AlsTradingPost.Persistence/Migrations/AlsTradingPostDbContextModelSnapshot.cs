﻿// <auto-generated />
using System;
using AlsTradingPost.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlsTradingPost.Persistence.Migrations
{
    [DbContext(typeof(AlsTradingPostDbContext))]
    partial class AlsTradingPostDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "2.1.0-rc1-32029")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlsTradingPost.Domain.Models.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("AlsTradingPost.Domain.Models.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuditedObject");

                    b.Property<Guid>("EntityId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("AlsTradingPost.Domain.Models.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Class")
                        .HasMaxLength(20);

                    b.Property<byte>("Level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Race")
                        .HasMaxLength(20);

                    b.Property<Guid?>("TraderId");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("TraderId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("AlsTradingPost.Domain.Models.MagicItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CharacterId");

                    b.Property<bool>("ForTrade");

                    b.Property<Guid?>("MagicItemTemplateId");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("MagicItemTemplateId");

                    b.ToTable("MagicItems");
                });

            modelBuilder.Entity("AlsTradingPost.Domain.Models.MagicItemTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("Rarity");

                    b.Property<bool>("Verified");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("MagicItemTemplates");
                });

            modelBuilder.Entity("AlsTradingPost.Domain.Models.Trader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("DCI")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Traders");
                });

            modelBuilder.Entity("AlsTradingPost.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdentityId");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Common.Authentication.Domain.Models.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsRevoked");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(100);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("AlsTradingPost.Domain.Models.Character", b =>
                {
                    b.HasOne("AlsTradingPost.Domain.Models.Trader", "Trader")
                        .WithMany()
                        .HasForeignKey("TraderId");
                });

            modelBuilder.Entity("AlsTradingPost.Domain.Models.MagicItem", b =>
                {
                    b.HasOne("AlsTradingPost.Domain.Models.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId");

                    b.HasOne("AlsTradingPost.Domain.Models.MagicItemTemplate", "MagicItemTemplate")
                        .WithMany()
                        .HasForeignKey("MagicItemTemplateId");
                });
#pragma warning restore 612, 618
        }
    }
}