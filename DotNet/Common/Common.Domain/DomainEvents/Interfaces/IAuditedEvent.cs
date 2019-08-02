using Common.Domain.Entities;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IAuditedEvent : IDomainEvent
    {
        IEntity Entity { get; set; }
    }
}