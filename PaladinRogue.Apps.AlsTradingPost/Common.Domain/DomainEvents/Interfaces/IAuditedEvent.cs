using Common.Domain.Models.Interfaces;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IAuditedEvent : IDomainEvent
    {
        IEntity Entity { get; set; }
    }
}