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
    [Migration("20190714212727_UseCompositeKeyForIdentityClaims")]
    partial class UseCompositeKeyForIdentityClaims
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("apps")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationManager.Domain.AuthenticationServices.AuthenticationService", b =>
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

            modelBuilder.Entity("ApplicationManager.Domain.Identities.Claim", b =>
                {
                    b.Property<string>("Type")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(80);

                    b.Property<Guid>("IdentityId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("Type");

                    b.HasIndex("IdentityId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.Identity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Version");

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

                    b.Property<int>("Version");

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

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypeClientCredential", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.AuthenticationServices.AuthenticationService");

                    b.Property<string>("AppAccessToken")
                        .IsRequired()
                        .HasMaxLength(1024);

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

            modelBuilder.Entity("ApplicationManager.Domain.Identities.ClientCredentialIdentity", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.Identities.AuthenticationIdentity");

                    b.Property<Guid>("AuthenticationGrantTypeClientCredentialId");

                    b.Property<string>("IdentifierHash")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.HasIndex("AuthenticationGrantTypeClientCredentialId");

                    b.HasDiscriminator().HasValue("CLIENT_CREDENTIAL");
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.PasswordIdentity", b =>
                {
                    b.HasBaseType("ApplicationManager.Domain.Identities.AuthenticationIdentity");

                    b.Property<Guid?>("AuthenticationGrantTypePasswordId");

                    b.Property<string>("EmailAddressHash")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasIndex("AuthenticationGrantTypePasswordId");

                    b.HasDiscriminator().HasValue("PASSWORD");
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

                    b.Property<DateTime>("TokenExpiry");

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

            modelBuilder.Entity("ApplicationManager.Domain.Identities.Claim", b =>
                {
                    b.HasOne("ApplicationManager.Domain.Identities.Identity", "Identity")
                        .WithMany("Claims")
                        .HasForeignKey("IdentityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.Session", b =>
                {
                    b.HasOne("ApplicationManager.Domain.Identities.Identity", "Identity")
                        .WithOne("Session")
                        .HasForeignKey("ApplicationManager.Domain.Identities.Session", "IdentityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("ApplicationManager.Domain.Identities.RefreshToken", "RefreshToken", b1 =>
                        {
                            b1.Property<Guid>("SessionId");

                            b1.Property<Guid?>("AuthenticationGrantTypeRefreshTokenId");

                            b1.Property<DateTime>("TokenExpiry");

                            b1.HasKey("SessionId");

                            b1.HasIndex("AuthenticationGrantTypeRefreshTokenId");

                            b1.ToTable("RefreshTokens","apps");

                            b1.HasOne("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypeRefreshToken", "AuthenticationGrantTypeRefreshToken")
                                .WithMany()
                                .HasForeignKey("AuthenticationGrantTypeRefreshTokenId");

                            b1.HasOne("ApplicationManager.Domain.Identities.Session", "Session")
                                .WithOne("RefreshToken")
                                .HasForeignKey("ApplicationManager.Domain.Identities.RefreshToken", "SessionId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("Common.Domain.DataProtection.HashSet", "TokenHash", b2 =>
                                {
                                    b2.Property<Guid>("RefreshTokenSessionId");

                                    b2.Property<string>("Hash")
                                        .IsRequired()
                                        .HasMaxLength(1024);

                                    b2.Property<string>("Salt")
                                        .IsRequired()
                                        .HasMaxLength(255);

                                    b2.HasKey("RefreshTokenSessionId");

                                    b2.ToTable("RefreshTokens","apps");

                                    b2.HasOne("ApplicationManager.Domain.Identities.RefreshToken")
                                        .WithOne("TokenHash")
                                        .HasForeignKey("Common.Domain.DataProtection.HashSet", "RefreshTokenSessionId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
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

            modelBuilder.Entity("ApplicationManager.Domain.Identities.ClientCredentialIdentity", b =>
                {
                    b.HasOne("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypeClientCredential", "AuthenticationGrantTypeClientCredential")
                        .WithMany()
                        .HasForeignKey("AuthenticationGrantTypeClientCredentialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationManager.Domain.Identities.PasswordIdentity", b =>
                {
                    b.HasOne("ApplicationManager.Domain.AuthenticationServices.AuthenticationGrantTypePassword", "AuthenticationGrantTypePassword")
                        .WithMany()
                        .HasForeignKey("AuthenticationGrantTypePasswordId");

                    b.OwnsOne("Common.Domain.DataProtection.HashSet", "PasswordHash", b1 =>
                        {
                            b1.Property<Guid>("PasswordIdentityId");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(1024);

                            b1.Property<string>("Salt")
                                .IsRequired()
                                .HasMaxLength(255);

                            b1.HasKey("PasswordIdentityId");

                            b1.ToTable("AuthenticationIdentities","apps");

                            b1.HasOne("ApplicationManager.Domain.Identities.PasswordIdentity")
                                .WithOne("PasswordHash")
                                .HasForeignKey("Common.Domain.DataProtection.HashSet", "PasswordIdentityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
