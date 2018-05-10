using AlsTradingPost.Domain.Models;
using Common.Authentication.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence
{
    public class AlsTradingPostDbContext : DbContext
    {
        public AlsTradingPostDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Audit> Audits { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Session> Sessions { get; set; }
        
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Trader> Traders { get; set; }

        public DbSet<Character> Characters { get; set; }

        public DbSet<MagicItem> MagicItems { get; set; }

        public DbSet<MagicItemTemplate> MagicItemTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}
