using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Persistence.EntityFramework.Repositories;

namespace AlsTradingPost.Persistence.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public AuditRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public void Add(Audit entity)
        {
            RepositoryHelper.Add(_context.Audits, _context, entity);
        }
    }
}