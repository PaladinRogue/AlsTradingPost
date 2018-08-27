using System.Collections.Generic;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventHandlerResolver
    {
        IEnumerable<IDomainEventHandler<T>> ResolveAll<T>() where T : IDomainEvent;
    }
}
