using Vault.Domain;
using Vault.Domain.Applications;
using Vault.Domain.SharedDataKeys;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.Extensions;
using Persistence.EntityFramework.Infrastructure.SecurityKeys;

namespace Vault.Persistence
{
    public class VaultDbContext : DbContext
    {
        public VaultDbContext(DbContextOptions<VaultDbContext> options) : base(options)
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
                .HasConversion(SymmetricSecurityKeyConverter.Create(MasterDataKeys.Master));

            modelBuilder.Entity<ApplicationDataKey>()
                .ToTable("ApplicationDataKeys")
                .ProtectSensitiveInformation()
                .Property(a => a.Value)
                .HasConversion(SymmetricSecurityKeyConverter.Create(MasterDataKeys.Master));
        }
    }
}