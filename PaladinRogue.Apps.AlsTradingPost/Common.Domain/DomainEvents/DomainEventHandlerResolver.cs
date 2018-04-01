using Common.Domain.DomainEvents.Interfaces;
using System;
using System.Collections.Generic;

namespace Common.Domain.DomainEvents
{
	public class DomainEventHandlerResolver : IDomainEventHandlerResolver
	{
		public IEnumerable<Delegate> ResolveAllOfType(Type type)
		{
			return DomainEventHandlerFactory.DomainEventTypeHandlers[type];
		}
	}
}