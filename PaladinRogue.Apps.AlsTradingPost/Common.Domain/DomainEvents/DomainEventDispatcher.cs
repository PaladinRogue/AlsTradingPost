using System;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
	    private readonly IDomainEvents _domainEvents;

		public DomainEventDispatcher(IDomainEvents domainEvents)
		{
			_domainEvents = domainEvents;
		}

	    public void DispatchEvents()
	    {
		    foreach (IDomainEvent domainEvent in _domainEvents.GetAll())
		    {
			    foreach (Delegate domainEventHandler in DomainEventHandlerFactory.GetAllOfType(domainEvent.GetType()))
			    {
				   domainEventHandler.DynamicInvoke(domainEvent);
			    }
		    }
	    }
    }
}
