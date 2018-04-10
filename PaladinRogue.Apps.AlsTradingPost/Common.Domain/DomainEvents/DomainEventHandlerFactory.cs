using System;
using System.Collections.Generic;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
	public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
	    private readonly IEnumerable<IDomainEventHandler> _handlers;

	    public DomainEventHandlerFactory(IEnumerable<IDomainEventHandler> handlers)
	    {
	        _handlers = handlers;
	    }

	    public void Initialise()
	    {
	        foreach (IDomainEventHandler messageSubscriber in _handlers)
	        {
	            messageSubscriber.Register();
	        }
	    }
    }
}
