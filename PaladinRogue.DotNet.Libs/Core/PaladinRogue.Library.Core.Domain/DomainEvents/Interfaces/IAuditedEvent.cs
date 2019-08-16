using PaladinRogue.Library.Core.Domain.Entities;

namespace PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces
{
    public interface IAuditedEvent : IDomainEvent
    {
        IEntity Entity { get; set; }
    }
}