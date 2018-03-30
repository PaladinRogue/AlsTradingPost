using System.Collections.Generic;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEventOrchestrator : IDomainEventOrchestrator
    {
	    private readonly IDomainEventHandlerResolver _domainEventHandlerResolver;

	    public DomainEventOrchestrator(IDomainEventHandlerResolver domainEventHandlerResolver)
	    {
		    _domainEventHandlerResolver = domainEventHandlerResolver;
	    }

	    public IList<IDomainEvent> DomainEvents { get; set; }

	    public void DispatchEvents()
	    {
		    foreach (IDomainEvent domainEvent in DomainEvents)
		    {
			    foreach (var domainEventHandler in _domainEventHandlerResolver.ResolveAllOfType(domainEvent.GetType()))
			    {
					domainEventHandler.Handle(domainEvent);
				}
		    }
	    }
    }
}
