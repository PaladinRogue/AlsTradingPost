using ApplicationManager.Domain.Applications;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.AuthenticationServices.Identities;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Sessions;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("apps");

            modelBuilder.ProtectSensitiveInformation();

            modelBuilder.Entity<PasswordIdentity>()
                .ToTable("PasswordIdentites");

            modelBuilder.Entity<Session>()
                .ToTable("Sessions");

            modelBuilder.Entity<AuthenticationService>()
                .HasDiscriminator(a => a.Type);

            modelBuilder.Entity<AuthenticationGrantTypeRefreshToken>()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<AuthenticationGrantTypePassword>()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<AuthenticationGrantTypeClientCredential>()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<Identity>()
                .HasOne(i => i.Session)
                .WithOne(s => s.Identity)
                .HasForeignKey<Session>(s => s.IdentityId);
        }
    }
}
