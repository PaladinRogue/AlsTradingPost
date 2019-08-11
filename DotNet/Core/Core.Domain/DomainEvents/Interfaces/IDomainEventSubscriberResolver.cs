using System.Collections.Generic;

namespace PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventSubscriberResolver
    {
        IEnumerable<IDomainEventSubscriber<T>> ResolveAll<T>() where T : IDomainEvent;
    }
}
