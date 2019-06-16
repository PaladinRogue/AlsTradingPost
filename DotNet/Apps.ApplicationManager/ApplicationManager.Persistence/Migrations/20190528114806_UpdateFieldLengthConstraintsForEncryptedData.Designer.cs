﻿// <auto-generated />
using System;
using ApplicationManager.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationManager.Persistence.Migrations
{
    [DbContext(typeof(ApplicationManagerDbContext))]
    [Migration("20190528114806_UpdateFieldLengthConstraintsForEncryptedData")]
    partial class UpdateFieldLengthConstraintsForEncryptedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("apps")
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationManager.Domain.Applications.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("SystemName")
                        .HasMaxLength(20);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("ApplicationManager.Domain.AuthenticationServices.AuthenticationService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("AuthenticationServices");

                    b.HasDiscriminator<string>("Type").HasValue("AuthenticationService");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.AuthenticationIdentities.AuthenticationIdentity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("IdentityId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdentityId");

                    b.ToTable("AuthenticationIdentities");

                    b.HasDiscriminator<string>("Type").HasValue("AuthenticationIdentity");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.Identity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.Sessions.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdentityId");

                    b.Property<bool>("IsRevoked");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypeClientCredential", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.AuthenticationServices.AuthenticationService");

                    b.Property<string>("ClientGrantAccessTokenUrl");

                    b.Property<string>("ClientId")
                        .HasMaxLength(1024);

                    b.Property<string>("ClientSecret")
                        .HasMaxLength(1024);

                    b.Property<string>("GrantAccessTokenUrl");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("ValidateAccessTokenUrl");

                    b.ToTable("AuthenticationGrantTypeClientCredential");

                    b.HasDiscriminator().HasValue("AuthenticationGrantTypeClientCredential");
                });

            modelBuilder.Entity("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypePassword", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.AuthenticationServices.AuthenticationService");


                    b.ToTable("AuthenticationGrantTypePassword");

                    b.HasDiscriminator().HasValue("AuthenticationGrantTypePassword");
                });

            modelBuilder.Entity("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypeRefreshToken", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.AuthenticationServices.AuthenticationService");


                    b.ToTable("AuthenticationGrantTypeRefreshToken");

                    b.HasDiscriminator().HasValue("AuthenticationGrantTypeRefreshToken");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.AuthenticationIdentities.PasswordIdentity", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.Identities.AuthenticationIdentities.AuthenticationIdentity");

                    b.Property<Guid?>("AuthenticationGrantTypePasswordId");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Password")
                        .HasMaxLength(1024);

                    b.HasIndex("AuthenticationGrantTypePasswordId");

                    b.ToTable("PasswordIdentity");

                    b.HasDiscriminator().HasValue("PasswordIdentity");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.AuthenticationIdentities.TwoFactorAuthenticationIdentity", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.Identities.AuthenticationIdentities.AuthenticationIdentity");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(1024);

                    b.Property<string>("Token")
                        .HasMaxLength(1024);

                    b.ToTable("TwoFactorAuthenticationIdentity");

                    b.HasDiscriminator().HasValue("TwoFactorAuthenticationIdentity");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.AuthenticationIdentities.AuthenticationIdentity", b =>
                {
                    b.HasOne("ApplicationManager.Domain.Identities.Identity", "Identity")
                        .WithMany("AuthenticationIdentities")
                        .HasForeignKey("IdentityId");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.Sessions.Session", b =>
                {
                    b.HasOne("ApplicationManager.Domain.Identities.Identity", "Identity")
                        .WithOne("Session")
                        .HasForeignKey("ApplicationManager.Domain.Identities.Sessions.Session", "IdentityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.AuthenticationIdentities.PasswordIdentity", b =>
                {
                    b.HasOne("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypePassword", "AuthenticationGrantTypePassword")
                        .WithMany()
                        .HasForeignKey("AuthenticationGrantTypePasswordId");
                });
#pragma warning restore 612, 618
        }
    }
}