using Microsoft.EntityFrameworkCore;
using PaladinRogue.Gateway.Domain.Applications;
using PaladinRogue.Libray.Persistence.EntityFramework.Infrastructure.Extensions;
using PaladinRogue.Libray.Persistence.EntityFramework.Infrastructure.Uris;

namespace PaladinRogue.Gateway.Persistence
{
    public class GatewayDbContext : DbContext
    {
        public GatewayDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("gateway");

            modelBuilder.ProtectSensitiveInformation();

            modelBuilder.Entity<Application>()
                .Property(a => a.HostUri)
                .HasConversion(UriConverter.Create());
        }
    }
}