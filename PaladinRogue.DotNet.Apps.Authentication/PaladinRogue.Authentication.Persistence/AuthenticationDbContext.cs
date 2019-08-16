using Microsoft.EntityFrameworkCore;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.NotificationTypes;
using PaladinRogue.Authentication.Domain.Users;
using PaladinRogue.Authentication.Persistence.Identities;
using PaladinRogue.Authentication.Persistence.NotificationTypes;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.DateTimeConverters;
using PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.Extensions;
using PaladinRogue.Library.ReferenceData.Domain;
using PaladinRogue.Library.ReferenceData.Persistence;
using AuthenticationGrantTypes = PaladinRogue.Authentication.Persistence.AuthenticationServices.AuthenticationGrantTypes;
using Identity = PaladinRogue.Authentication.Domain.Identities.Identity;

namespace PaladinRogue.Authentication.Persistence
{
    public partial class AuthenticationDbContext : DbContext, IReferenceDataDbContext
    {
        public AuthenticationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AuthenticationService> AuthenticationServices { get; set; }

        public DbSet<Identity> Identities { get; set; }

        public DbSet<NotificationType> NotificationTypes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ReferenceDataType> ReferenceDataTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("authentication");

            modelBuilder.UseReferenceData();

            modelBuilder.ProtectSensitiveInformation();

            modelBuilder.Entity<AuthenticationIdentity>()
                .ToTable("AuthenticationIdentities");

            modelBuilder.Entity<Session>()
                .ToTable("Sessions")
                .OwnsOne(i => i.RefreshToken);

            modelBuilder.Entity<Claim>()
                .ToTable("Claims")
                .HasKey(c => new {c.Type, c.IdentityId});

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
                .HasValue<AuthenticationGrantTypeFacebook>(AuthenticationGrantTypes.Facebook)
                .HasValue<AuthenticationGrantTypeGoogle>(AuthenticationGrantTypes.Google)
                .HasValue<AuthenticationGrantTypeRefreshToken>(AuthenticationGrantTypes.RefreshToken);

            modelBuilder.Entity<AuthenticationGrantTypeRefreshToken>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<AuthenticationGrantTypePassword>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<AuthenticationGrantTypeFacebook>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationGrantTypeClientCredential>();

            modelBuilder.Entity<AuthenticationGrantTypeGoogle>()
                .ProtectSensitiveInformation()
                .HasBaseType<AuthenticationGrantTypeClientCredential>();

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