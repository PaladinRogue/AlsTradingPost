using System.Collections.Generic;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEventFactory : IDomainEventFactory
    {
	    private readonly IList<IDomainEvent> _domainEvents;

	    public DomainEventFactory()
	    {
		    _domainEvents = new List<IDomainEvent>();
	    }

	    public void Raise(IDomainEvent domainEvent)
	    {
		    _domainEvents.Add(domainEvent);
		}

	    public IEnumerable<IDomainEvent> GetAll()
	    {
		    return _domainEvents;

	    }
    }
}
