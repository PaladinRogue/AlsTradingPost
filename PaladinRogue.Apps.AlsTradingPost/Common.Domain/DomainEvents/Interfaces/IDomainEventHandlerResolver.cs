using System;
using System.Collections.Generic;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventHandlerResolver
    {
	    IList<IDomainEventHandler<IDomainEvent>> DomainEventHandlers { get; set; }

	    IEnumerable<IDomainEventHandler<IDomainEvent>> ResolveAllOfType(Type type);
    }
}
