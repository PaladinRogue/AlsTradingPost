﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReverseProxy.Persistence;

namespace ReverseProxy.Persistence.Migrations
{
    [DbContext(typeof(ReverseProxyDbContext))]
    [Migration("20190719101539_UpdateSchemaName")]
    partial class UpdateSchemaName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("proxy")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReverseProxy.Domain.Applications.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HostUri")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });
#pragma warning restore 612, 618
        }
    }
}