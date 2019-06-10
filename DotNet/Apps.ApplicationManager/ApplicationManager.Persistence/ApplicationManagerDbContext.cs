using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.Domain.Applications;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;
using ApplicationManager.Domain.Identities.Sessions;
using ApplicationManager.Domain.NotificationTypes;
using ApplicationManager.Persistence.Identities;
using ApplicationManager.Persistence.NotificationTypes;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.Extensions;

namespace ApplicationManager.Persistence
{
    public class ApplicationManagerDbContext : DbContext
    {
        public ApplicationManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<AuthenticationService> AuthenticationServices { get; set; }

        public DbSet<Identity> Identities { get; set; }

        public DbSet<NotificationType> NotificationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("apps");

            modelBuilder.ProtectSensitiveInformation();

            modelBuilder.Entity<AuthenticationIdentity>()
                .ToTable("AuthenticationIdentities");

            modelBuilder.Entity<Session>()
                .ToTable("Sessions");

            modelBuilder.Entity<AuthenticationService>()
                .HasDiscriminator(a => a.Type);

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
                .HasValue<TwoFactorAuthenticationIdentity>(AuthenticationIdentityTypes.TwoFactor);

            modelBuilder.Entity<PasswordIdentity>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationIdentity>();

            modelBuilder.Entity<TwoFactorAuthenticationIdentity>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationIdentity>();

            modelBuilder.Entity<Identity>()
                .HasOne(i => i.Session)
                .WithOne(s => s.Identity)
                .HasForeignKey<Session>(s => s.IdentityId);

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
            
            modelBuilder.Query<TwoFactorAuthenticationIdentityProjection>()
                .ProtectSensitiveInformation()
                .ToView("TwoFactorAuthenticationIdentityProjection");
        }
    }
}
