using AlsTradingPost.Domain.AuditServices.Models;

namespace AlsTradingPost.Domain.AuditServices.Interfaces
{
    public interface IAuditCommandService
    {
        void AuditEntity(AuditEntityDdto auditEntityDdto);
    }
}
