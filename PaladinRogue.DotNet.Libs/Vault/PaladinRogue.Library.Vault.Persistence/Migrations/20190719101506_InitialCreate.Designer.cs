﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaladinRogue.Library.Vault.Persistence.Migrations
{
    [DbContext(typeof(VaultDbContext))]
    [Migration("20190719101506_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("vault")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vault.Domain.Applications.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Vault.Domain.Applications.ApplicationDataKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationId");

                    b.Property<int>("Type");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ApplicationDataKey");
                });

            modelBuilder.Entity("Vault.Domain.SharedDataKeys.SharedDataKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Type");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("SharedDataKeys");
                });

            modelBuilder.Entity("Vault.Domain.Applications.ApplicationDataKey", b =>
                {
                    b.HasOne("Vault.Domain.Applications.Application", "Application")
                        .WithMany("ApplicationDataKeys")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
