using System.Collections.Generic;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class PendingDomainEventDirector : IPendingDomainEventDirector
    {
	    private readonly IList<IDomainEvent> _domainEvents;

	    public PendingDomainEventDirector()
	    {
	        _domainEvents = new List<IDomainEvent>();
	    }

	    public void Add(IDomainEvent domainEvent)
	    {
	        _domainEvents.Add(domainEvent);
		}

	    public IEnumerable<IDomainEvent> GetAll()
	    {
		    return _domainEvents;
	    }
    }
}
