using ApplicationManager.Domain.Applications;
using ApplicationManager.Domain.AuthenticationServices;
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

        public DbSet<ApplicationAuthenticationService> ApplicationAuthenticationServices { get; set; }

        public DbSet<AuthenticationService> AuthenticationServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ProtectSensitiveInformation();

            modelBuilder.Entity<AuthenticationGrantTypePassword>()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<AuthenticationGrantTypeClientCredential>()
                .HasBaseType<AuthenticationService>();

            modelBuilder.Entity<ApplicationAuthenticationService>()
                .HasKey(a => new { a.ApplicationId, a.AuthenticationServiceId });

            modelBuilder.Entity<ApplicationAuthenticationService>()
                .HasOne<AuthenticationService>()
                .WithMany()
                .HasForeignKey(a => a.AuthenticationServiceId);
        }
    }
}
