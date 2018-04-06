using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Persistence.Interfaces
{
    public interface IAuditRepository
    {
        void AuditEntity(IEntity entity);
    }
}