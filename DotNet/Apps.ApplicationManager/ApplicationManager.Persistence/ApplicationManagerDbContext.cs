﻿using ApplicationManager.Domain.Applications;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.NotificationTypes;
using ApplicationManager.Domain.Users;
using ApplicationManager.Persistence.AuthenticationServices;
using ApplicationManager.Persistence.Identities;
using ApplicationManager.Persistence.NotificationTypes;
using Common.Domain.Models.DataProtection;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.DateTimeConverters;
using Persistence.EntityFramework.Infrastructure.Extensions;
using Identity = ApplicationManager.Domain.Identities.Identity;

namespace ApplicationManager.Persistence
{
    public partial class ApplicationManagerDbContext : DbContext
    {
        public ApplicationManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<AuthenticationService> AuthenticationServices { get; set; }

        public DbSet<Identity> Identities { get; set; }

        public DbSet<NotificationType> NotificationTypes { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("apps");

            modelBuilder.ProtectSensitiveInformation();

            modelBuilder.Entity<AuthenticationIdentity>()
                .ToTable("AuthenticationIdentities");

            modelBuilder.Entity<Session>()
                .ToTable("Sessions");

            modelBuilder.Entity<AuthenticationService>()
                .HasDiscriminator<string>("Type")
                .HasValue<AuthenticationGrantTypePassword>(AuthenticationGrantTypes.Password)
                .HasValue<AuthenticationGrantTypeClientCredential>(AuthenticationGrantTypes.ClientCredential)
                .HasValue<AuthenticationGrantTypeRefreshToken>(AuthenticationGrantTypes.RefreshToken);

            modelBuilder.Entity<AuthenticationGrantTypeRefreshToken>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<AuthenticationGrantTypePassword>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<AuthenticationGrantTypeClientCredential>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<AuthenticationIdentity>()
                .HasDiscriminator<string>("Type")
                .HasValue<PasswordIdentity>(AuthenticationIdentityTypes.Password)
                .HasValue<TwoFactorAuthenticationIdentity>(AuthenticationIdentityTypes.TwoFactor)
                .HasValue<RefreshTokenIdentity>(AuthenticationIdentityTypes.RefreshToken);

            modelBuilder.Entity<PasswordIdentity>()
                .Ignore(p => p.Password)
                .HasBaseType<AuthenticationIdentity>()
                .ProtectSensitiveInformation()
                .OwnsOne(typeof(HashSet), "PasswordHash");

            modelBuilder.Entity<TwoFactorAuthenticationIdentity>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationIdentity>()
                .Property(t => t.TokenExpiry)
                .HasConversion(InstantConverter.Create());

            modelBuilder.Entity<RefreshTokenIdentity>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationIdentity>();

            modelBuilder.Entity<Identity>()
                .HasOne(i => i.Session)
                .WithOne(s => s.Identity)
                .HasForeignKey<Session>("IdentityId");

            modelBuilder.Entity<Identity>()
                .Ignore(i => i.EmailAddress);

            modelBuilder.Entity<NotificationTypeChannel>()
                .ToTable("NotificationTypeChannels");

            modelBuilder.Entity<NotificationChannelTemplate>()
                .ToTable("NotificationChannelTemplates")
                .HasDiscriminator<string>("Type")
                .HasValue<EmailChannelTemplate>(ChannelTemplateTypes.Email);

            modelBuilder.Entity<EmailChannelTemplate>()
                .HasBaseType<NotificationChannelTemplate>();

            modelBuilder.Entity<NotificationTypeChannel>()
                .HasOne(i => i.NotificationChannelTemplate)
                .WithOne(s => s.NotificationTypeChannel)
                .HasForeignKey<NotificationChannelTemplate>("NotificationTypeChannelId");

            modelBuilder.Entity<User>()
                .ToTable("Users");

            ConfigureQueryTypes(modelBuilder);
        }
    }
}
