using System;
using System.Collections.Generic;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventHandlerResolver
    {
	    IEnumerable<Delegate> ResolveAllOfType(Type type);
    }
}
