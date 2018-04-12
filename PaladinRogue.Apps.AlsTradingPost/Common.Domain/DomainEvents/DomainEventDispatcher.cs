using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
	public class DomainEventDispatcher : IDomainEventDispatcher
	{
	    private readonly IPendingDomainEventDirector _pendingDomainEventDirector;
	    private readonly IDomainEventBus _domainEventBus;

        public DomainEventDispatcher(IDomainEventBus domainEventBus, IPendingDomainEventDirector pendingDomainEventDirector)
        {
            _domainEventBus = domainEventBus;
            _pendingDomainEventDirector = pendingDomainEventDirector;
        }

		public async Task DispatchEventsAsync()
		{
		    await Task.Run(() => Parallel.ForEach(_pendingDomainEventDirector.GetAll(),
		        message => { _domainEventBus.Publish(message); }));
        }
	}
}
