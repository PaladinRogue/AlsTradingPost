using Authentication.Domain.AuthenticationServices;
using Authentication.Domain.Identities;
using Authentication.Domain.NotificationTypes;
using Authentication.Domain.Users;
using Authentication.Persistence.AuthenticationServices;
using Authentication.Persistence.Identities;
using Authentication.Persistence.NotificationTypes;
using Common.Domain.DataProtectors;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.DateTimeConverters;
using Persistence.EntityFramework.Infrastructure.Extensions;
using Identity = Authentication.Domain.Identities.Identity;

namespace Authentication.Persistence
{
    public partial class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions options) : base(options)
        {
        }

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
                .ToTable("Sessions")
                .OwnsOne(i => i.RefreshToken);

            modelBuilder.Entity<Claim>()
                .ToTable("Claims")
                // Can't set composite key on navigation property through fluent api so have done so
                // by changing the migration manually
                .HasKey(c => new { c.Type, c.Value/*, c.Identity.Id*/ });

            modelBuilder.Entity<RefreshToken>()
                .ToTable("RefreshTokens")
                .Ignore(p => p.Token)
                .ProtectSensitiveInformation()
                .OwnsOne(typeof(HashSet), "TokenHash");

            modelBuilder.Entity<RefreshToken>()
                .Property(t => t.TokenExpiry)
                .HasConversion(InstantConverter.Create());

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
                .HasValue<ClientCredentialIdentity>(AuthenticationIdentityTypes.ClientCredential);

            modelBuilder.Entity<PasswordIdentity>()
                .Ignore(p => p.Password)
                .Ignore(i => i.EmailAddress)
                .HasBaseType<AuthenticationIdentity>()
                .ProtectSensitiveInformation()
                .OwnsOne(typeof(HashSet), "PasswordHash");

            modelBuilder.Entity<TwoFactorAuthenticationIdentity>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationIdentity>()
                .Property(t => t.TokenExpiry)
                .HasConversion(InstantConverter.Create());

            modelBuilder.Entity<ClientCredentialIdentity>()
                .Ignore(i => i.Identifier)
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationIdentity>();

            modelBuilder.Entity<Identity>()
                .HasOne(i => i.Session)
                .WithOne(s => s.Identity)
                .HasForeignKey<Session>("IdentityId");

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