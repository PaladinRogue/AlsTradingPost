using Microsoft.EntityFrameworkCore;
using PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.Extensions;
using PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.SecurityKeys;
using PaladinRogue.Library.Vault.Domain;
using PaladinRogue.Library.Vault.Domain.Applications;
using PaladinRogue.Library.Vault.Domain.SharedDataKeys;

namespace PaladinRogue.Library.Vault.Persistence
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