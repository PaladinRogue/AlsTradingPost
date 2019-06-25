﻿// <auto-generated />
using System;
using ApplicationManager.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationManager.Persistence.Migrations
{
    [DbContext(typeof(ApplicationManagerDbContext))]
    partial class ApplicationManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("apps")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationManager.Domain.Applications.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("SystemName")
                        .IsRequired()
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

            modelBuilder.Entity("ApplicationManager.Domain.Identities.AuthenticationIdentity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdentityId");

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

                    b.Property<string>("EmailAddressHash")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdentityId");

                    b.Property<bool>("IsRevoked");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("ApplicationManager.Domain.NotificationTypes.NotificationChannelTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("NotificationTypeChannelId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("NotificationTypeChannelId")
                        .IsUnique()
                        .HasFilter("[NotificationTypeChannelId] IS NOT NULL");

                    b.ToTable("NotificationChannelTemplates");

                    b.HasDiscriminator<string>("Type").HasValue("NotificationChannelTemplate");
                });

            modelBuilder.Entity("ApplicationManager.Domain.NotificationTypes.NotificationType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("NotificationTypes");
                });

            modelBuilder.Entity("ApplicationManager.Domain.NotificationTypes.NotificationTypeChannel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChannelType");

                    b.Property<Guid?>("NotificationTypeId");

                    b.HasKey("Id");

                    b.HasIndex("NotificationTypeId");

                    b.ToTable("NotificationTypeChannels");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("IdentityId");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("IdentityId");

                    b.ToTable("Users");
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

                    b.HasDiscriminator().HasValue("CLIENT_CREDENTIAL");
                });

            modelBuilder.Entity("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypePassword", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.AuthenticationServices.AuthenticationService");

                    b.HasDiscriminator().HasValue("PASSWORD");
                });

            modelBuilder.Entity("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypeRefreshToken", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.AuthenticationServices.AuthenticationService");

                    b.HasDiscriminator().HasValue("REFRESH_TOKEN");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.PasswordIdentity", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.Identities.AuthenticationIdentity");

                    b.Property<Guid?>("AuthenticationGrantTypePasswordId");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasIndex("AuthenticationGrantTypePasswordId");

                    b.HasDiscriminator().HasValue("PASSWORD");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.RefreshTokenIdentity", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.Identities.AuthenticationIdentity");

                    b.Property<Guid?>("AuthenticationGrantTypeRefreshTokenId");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.HasIndex("AuthenticationGrantTypeRefreshTokenId");

                    b.HasDiscriminator().HasValue("REFRESH_TOKEN");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.TwoFactorAuthenticationIdentity", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.Identities.AuthenticationIdentity");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<int>("TwoFactorAuthenticationType");

                    b.HasDiscriminator().HasValue("TWO_FACTOR");
                });

            modelBuilder.Entity("ApplicationManager.Domain.NotificationTypes.EmailChannelTemplate", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.NotificationTypes.NotificationChannelTemplate");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Template")
                        .IsRequired();

                    b.HasDiscriminator().HasValue("EMAIL");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.AuthenticationIdentity", b =>
                {
                    b.HasOne("ApplicationManager.Domain.Identities.Identity", "Identity")
                        .WithMany("AuthenticationIdentities")
                        .HasForeignKey("IdentityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.Session", b =>
                {
                    b.HasOne("ApplicationManager.Domain.Identities.Identity", "Identity")
                        .WithOne("Session")
                        .HasForeignKey("ApplicationManager.Domain.Identities.Session", "IdentityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationManager.Domain.NotificationTypes.NotificationChannelTemplate", b =>
                {
                    b.HasOne("ApplicationManager.Domain.NotificationTypes.NotificationTypeChannel", "NotificationTypeChannel")
                        .WithOne("NotificationChannelTemplate")
                        .HasForeignKey("ApplicationManager.Domain.NotificationTypes.NotificationChannelTemplate", "NotificationTypeChannelId");
                });

            modelBuilder.Entity("ApplicationManager.Domain.NotificationTypes.NotificationTypeChannel", b =>
                {
                    b.HasOne("ApplicationManager.Domain.NotificationTypes.NotificationType", "NotificationType")
                        .WithMany("NotificationTypeChannels")
                        .HasForeignKey("NotificationTypeId");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Users.User", b =>
                {
                    b.HasOne("ApplicationManager.Domain.Identities.Identity", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.PasswordIdentity", b =>
                {
                    b.HasOne("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypePassword", "AuthenticationGrantTypePassword")
                        .WithMany()
                        .HasForeignKey("AuthenticationGrantTypePasswordId");

                    b.OwnsOne("Common.Setup.Infrastructure.Hashing.HashSet", "PasswordHash", b1 =>
                        {
                            b1.Property<Guid>("PasswordIdentityId");

                            b1.Property<string>("Hash");

                            b1.Property<string>("Salt");

                            b1.HasKey("PasswordIdentityId");

                            b1.ToTable("AuthenticationIdentities","apps");

                            b1.HasOne("ApplicationManager.Domain.Identities.PasswordIdentity")
                                .WithOne("PasswordHash")
                                .HasForeignKey("Common.Setup.Infrastructure.Hashing.HashSet", "PasswordIdentityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.RefreshTokenIdentity", b =>
                {
                    b.HasOne("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypeRefreshToken", "AuthenticationGrantTypeRefreshToken")
                        .WithMany()
                        .HasForeignKey("AuthenticationGrantTypeRefreshTokenId");
                });
#pragma warning restore 612, 618
        }
    }
}
