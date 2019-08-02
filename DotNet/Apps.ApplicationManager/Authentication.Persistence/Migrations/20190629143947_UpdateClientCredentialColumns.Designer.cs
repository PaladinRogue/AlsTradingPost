﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Persistence.Migrations
{
    [DbContext(typeof(AuthenticationDbContext))]
    [Migration("20190629143947_UpdateClientCredentialColumns")]
    partial class UpdateClientCredentialColumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("apps")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Authentication.Domain.Applications.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Authentication.Domain.AuthenticationServices.AuthenticationService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("AuthenticationServices");

                    b.HasDiscriminator<string>("Type").HasValue("AuthenticationService");
                });

            modelBuilder.Entity("Authentication.Domain.Identities.AuthenticationIdentity", b =>
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

            modelBuilder.Entity("Authentication.Domain.Identities.Identity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddressHash")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("Authentication.Domain.Identities.Session", b =>
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

            modelBuilder.Entity("Authentication.Domain.NotificationTypes.NotificationChannelTemplate", b =>
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

            modelBuilder.Entity("Authentication.Domain.NotificationTypes.NotificationType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("NotificationTypes");
                });

            modelBuilder.Entity("Authentication.Domain.NotificationTypes.NotificationTypeChannel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChannelType");

                    b.Property<Guid?>("NotificationTypeId");

                    b.HasKey("Id");

                    b.HasIndex("NotificationTypeId");

                    b.ToTable("NotificationTypeChannels");
                });

            modelBuilder.Entity("Authentication.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("IdentityId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Authentication.Domain.AuthenticationServices.AuthenticationGrantTypeClientCredential", b =>
                {
                    b.HasBaseType("Authentication.Domain.AuthenticationServices.AuthenticationService");

                    b.Property<string>("ClientGrantAccessTokenUrl")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("GrantAccessTokenUrl")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("ValidateAccessTokenUrl")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasDiscriminator().HasValue("CLIENT_CREDENTIAL");
                });

            modelBuilder.Entity("Authentication.Domain.AuthenticationServices.AuthenticationGrantTypePassword", b =>
                {
                    b.HasBaseType("Authentication.Domain.AuthenticationServices.AuthenticationService");

                    b.HasDiscriminator().HasValue("PASSWORD");
                });

            modelBuilder.Entity("Authentication.Domain.AuthenticationServices.AuthenticationGrantTypeRefreshToken", b =>
                {
                    b.HasBaseType("Authentication.Domain.AuthenticationServices.AuthenticationService");

                    b.HasDiscriminator().HasValue("REFRESH_TOKEN");
                });

            modelBuilder.Entity("Authentication.Domain.Identities.PasswordIdentity", b =>
                {
                    b.HasBaseType("Authentication.Domain.Identities.AuthenticationIdentity");

                    b.Property<Guid?>("AuthenticationGrantTypePasswordId");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasIndex("AuthenticationGrantTypePasswordId");

                    b.HasDiscriminator().HasValue("PASSWORD");
                });

            modelBuilder.Entity("Authentication.Domain.Identities.RefreshTokenIdentity", b =>
                {
                    b.HasBaseType("Authentication.Domain.Identities.AuthenticationIdentity");

                    b.Property<Guid?>("AuthenticationGrantTypeRefreshTokenId");

                    b.Property<DateTime>("TokenExpiry");

                    b.HasIndex("AuthenticationGrantTypeRefreshTokenId");

                    b.HasDiscriminator().HasValue("REFRESH_TOKEN");
                });

            modelBuilder.Entity("Authentication.Domain.Identities.TwoFactorAuthenticationIdentity", b =>
                {
                    b.HasBaseType("Authentication.Domain.Identities.AuthenticationIdentity");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<DateTime>("TokenExpiry")
                        .HasColumnName("TwoFactorAuthenticationIdentity_TokenExpiry");

                    b.Property<int>("TwoFactorAuthenticationType");

                    b.HasDiscriminator().HasValue("TWO_FACTOR");
                });

            modelBuilder.Entity("Authentication.Domain.NotificationTypes.EmailChannelTemplate", b =>
                {
                    b.HasBaseType("Authentication.Domain.NotificationTypes.NotificationChannelTemplate");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Template")
                        .IsRequired();

                    b.HasDiscriminator().HasValue("EMAIL");
                });

            modelBuilder.Entity("Authentication.Domain.Identities.AuthenticationIdentity", b =>
                {
                    b.HasOne("Authentication.Domain.Identities.Identity", "Identity")
                        .WithMany("AuthenticationIdentities")
                        .HasForeignKey("IdentityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Authentication.Domain.Identities.Session", b =>
                {
                    b.HasOne("Authentication.Domain.Identities.Identity", "Identity")
                        .WithOne("Session")
                        .HasForeignKey("Authentication.Domain.Identities.Session", "IdentityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Authentication.Domain.NotificationTypes.NotificationChannelTemplate", b =>
                {
                    b.HasOne("Authentication.Domain.NotificationTypes.NotificationTypeChannel", "NotificationTypeChannel")
                        .WithOne("NotificationChannelTemplate")
                        .HasForeignKey("Authentication.Domain.NotificationTypes.NotificationChannelTemplate", "NotificationTypeChannelId");
                });

            modelBuilder.Entity("Authentication.Domain.NotificationTypes.NotificationTypeChannel", b =>
                {
                    b.HasOne("Authentication.Domain.NotificationTypes.NotificationType", "NotificationType")
                        .WithMany("NotificationTypeChannels")
                        .HasForeignKey("NotificationTypeId");
                });

            modelBuilder.Entity("Authentication.Domain.Users.User", b =>
                {
                    b.HasOne("Authentication.Domain.Identities.Identity", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId");
                });

            modelBuilder.Entity("Authentication.Domain.Identities.PasswordIdentity", b =>
                {
                    b.HasOne("Authentication.Domain.AuthenticationServices.AuthenticationGrantTypePassword", "AuthenticationGrantTypePassword")
                        .WithMany()
                        .HasForeignKey("AuthenticationGrantTypePasswordId");

                    b.OwnsOne("Common.Domain.DataProtection.HashSet", "PasswordHash", b1 =>
                        {
                            b1.Property<Guid>("PasswordIdentityId");

                            b1.Property<string>("Hash");

                            b1.Property<string>("Salt");

                            b1.HasKey("PasswordIdentityId");

                            b1.ToTable("AuthenticationIdentities","apps");

                            b1.HasOne("Authentication.Domain.Identities.PasswordIdentity")
                                .WithOne("PasswordHash")
                                .HasForeignKey("Common.Domain.DataProtection.HashSet", "PasswordIdentityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Authentication.Domain.Identities.RefreshTokenIdentity", b =>
                {
                    b.HasOne("Authentication.Domain.AuthenticationServices.AuthenticationGrantTypeRefreshToken", "AuthenticationGrantTypeRefreshToken")
                        .WithMany()
                        .HasForeignKey("AuthenticationGrantTypeRefreshTokenId");

                    b.OwnsOne("Common.Domain.DataProtection.HashSet", "RefreshTokenHash", b1 =>
                        {
                            b1.Property<Guid>("RefreshTokenIdentityId");

                            b1.Property<string>("Hash");

                            b1.Property<string>("Salt");

                            b1.HasKey("RefreshTokenIdentityId");

                            b1.ToTable("AuthenticationIdentities","apps");

                            b1.HasOne("Authentication.Domain.Identities.RefreshTokenIdentity")
                                .WithOne("RefreshTokenHash")
                                .HasForeignKey("Common.Domain.DataProtection.HashSet", "RefreshTokenIdentityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
