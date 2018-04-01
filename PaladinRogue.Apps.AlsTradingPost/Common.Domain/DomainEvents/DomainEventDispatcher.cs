using System;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
	    private readonly IDomainEventHandlerResolver _domainEventHandlerResolver;
	    private readonly IDomainEventFactory _domainEventFactory;

		public DomainEventDispatcher(IDomainEventHandlerResolver domainEventHandlerResolver, IDomainEventFactory domainEventFactory)
		{
			_domainEventHandlerResolver = domainEventHandlerResolver;
			_domainEventFactory = domainEventFactory;
		}

	    public void DispatchEvents()
	    {
		    foreach (IDomainEvent domainEvent in _domainEventFactory.GetAll())
		    {
			    foreach (Delegate domainEventHandler in _domainEventHandlerResolver.ResolveAllOfType(domainEvent.GetType()))
			    {
				   domainEventHandler.DynamicInvoke(domainEvent);
			    }
		    }
	    }
    }
}
