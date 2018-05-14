using Authentication.Domain.Models;
using Common.Authentication.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.Extensions;

namespace Authentication.Persistence
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Identity> Identities { get; set; }
        
        public DbSet<Application> Applications { get; set; }
        
        public DbSet<Session> Sessions { get; set; }
		
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            
            modelBuilder.ProtectSensitiveInformation();
        }
	}
}
