using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.Extensions;
using Persistence.EntityFramework.Infrastructure.Uris;
using Gateway.Domain.Applications;

namespace Gateway.Persistence
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