using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEventFactory : IDomainEventFectory
    {
	    private readonly DomainEventOrchestrator _domainEventOrchestrator;

		public DomainEventFactory(DomainEventOrchestrator domainEventOrchestrator)
		{
			_domainEventOrchestrator = domainEventOrchestrator;
		}

	    public void Raise(IDomainEvent domainEvent)
	    {
			_domainEventOrchestrator.DomainEvents.Add(domainEvent);
		}
    }
}
