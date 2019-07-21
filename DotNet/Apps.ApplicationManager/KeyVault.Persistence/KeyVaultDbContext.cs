using KeyVault.Domain.Applications;
using KeyVault.Domain.SharedDataKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.EntityFramework.Infrastructure.Extensions;
using Persistence.EntityFramework.Infrastructure.SecurityKeys;

namespace KeyVault.Persistence
{
    public class KeyVaultDbContext : DbContext
    {
        public KeyVaultDbContext(DbContextOptions<KeyVaultDbContext> options) : base(options)
        {
        }

        public DbSet<SharedDataKey> SharedDataKeys { get; set; }

        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("vault");

            modelBuilder.ProtectSensitiveInformation();

            modelBuilder.Entity<SharedDataKey>()
                .ToTable("SharedDataKeys")
                .Property(a => a.Value)
                .HasConversion(SymmetricSecurityKeyConverter.Create());

            modelBuilder.Entity<SharedDataKey>()
                .Property(a => a.Type)
                .HasConversion<string>();

            modelBuilder.Entity<ApplicationDataKey>()
                .ToTable("ApplicationDataKeys")
                .ProtectSensitiveInformation()
                .Property(a => a.Value)
                .HasConversion(SymmetricSecurityKeyConverter.Create());
        }
    }
}