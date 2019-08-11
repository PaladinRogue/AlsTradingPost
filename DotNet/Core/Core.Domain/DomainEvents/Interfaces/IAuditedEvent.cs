using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces
{
    public interface IAuditedEvent : IDomainEvent
    {
        IEntity Entity { get; set; }
    }
}