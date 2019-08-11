using Microsoft.EntityFrameworkCore;
using PaladinRogue.Libray.Persistence.EntityFramework.Infrastructure.Extensions;
using PaladinRogue.Libray.Persistence.EntityFramework.Infrastructure.SecurityKeys;
using PaladinRogue.Libray.Vault.Domain;
using PaladinRogue.Libray.Vault.Domain.Applications;
using PaladinRogue.Libray.Vault.Domain.SharedDataKeys;

namespace PaladinRogue.Libray.Vault.Persistence
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