using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.Extensions;
using Persistence.EntityFramework.Infrastructure.Uris;
using ReverseProxy.Domain.Applications;

namespace ReverseProxy.Persistence
{
    public class ReverseProxyDbContext : DbContext
    {
        public ReverseProxyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("apps");

            modelBuilder.ProtectSensitiveInformation();

            modelBuilder.Entity<Application>()
                .Property(a => a.HostUri)
                .HasConversion(UriConverter.Create());
        }
    }
}