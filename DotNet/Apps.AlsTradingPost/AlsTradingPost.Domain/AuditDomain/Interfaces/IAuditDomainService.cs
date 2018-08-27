using AlsTradingPost.Domain.AuditDomain.Models;

namespace AlsTradingPost.Domain.AuditDomain.Interfaces
{
    public interface IAuditDomainService
    {
        void AuditEntity(AuditEntityDdto auditEntity);
    }
}
