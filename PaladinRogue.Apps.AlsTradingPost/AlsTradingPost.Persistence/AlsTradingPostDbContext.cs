using AlsTradingPost.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence
{
    public class AlsTradingPostDbContext : DbContext
    {
        public AlsTradingPostDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Character> Characters { get; set; }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}
