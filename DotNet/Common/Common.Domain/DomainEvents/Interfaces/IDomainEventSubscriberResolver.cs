using System.Collections.Generic;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventSubscriberResolver
    {
        IEnumerable<IDomainEventSubscriber<T>> ResolveAll<T>() where T : IDomainEvent;
    }
}
