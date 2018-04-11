using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;

namespace AlsTradingPost.Persistence.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public AuditRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public void Add(Audit audit)
        {
            _context.Audits.Add(audit);

            _context.SaveChanges();
        }
    }
}