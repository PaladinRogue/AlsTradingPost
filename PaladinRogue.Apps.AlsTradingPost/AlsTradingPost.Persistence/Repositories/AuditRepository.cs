using AlsTradingPost.Domain.Models;
using AlsTradingPost.Persistence.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Persistence.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public AuditRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public void AuditEntity(IEntity auditedObject)
        {
            _context.Audits.Add(Audit.Create(auditedObject));

            _context.SaveChanges();
        }
    }
}