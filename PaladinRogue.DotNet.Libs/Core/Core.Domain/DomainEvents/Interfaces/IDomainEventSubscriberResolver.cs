using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventSubscriberResolver
    {
        IEnumerable<IDomainEventSubscriber<T>> ResolveAll<T>() where T : IDomainEvent;
    }
}
