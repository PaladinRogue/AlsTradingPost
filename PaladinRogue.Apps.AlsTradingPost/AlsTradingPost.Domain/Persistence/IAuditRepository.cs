using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.Persistence
{
    public interface IAuditRepository
    {
        void AuditEntity(IEntity entity);
    }
}