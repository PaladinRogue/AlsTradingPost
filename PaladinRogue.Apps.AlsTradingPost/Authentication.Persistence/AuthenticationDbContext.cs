using Authentication.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Identity> Identities { get; set; }
        
        public DbSet<Application> Applications { get; set; }
		
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
        }
	}
}
