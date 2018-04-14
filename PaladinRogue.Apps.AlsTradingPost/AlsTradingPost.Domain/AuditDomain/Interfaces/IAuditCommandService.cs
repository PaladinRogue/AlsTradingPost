using AlsTradingPost.Domain.AuditDomain.Models;

namespace AlsTradingPost.Domain.AuditDomain.Interfaces
{
    public interface IAuditCommandService
    {
        void AuditEntity(AuditEntityDdto auditEntityDdto);
    }
}
