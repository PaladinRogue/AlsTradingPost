using System;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventHandlerResolver
    {
        IDomainEventHandler<IDomainEvent> Resolve(Type handlerType);
    }
}
