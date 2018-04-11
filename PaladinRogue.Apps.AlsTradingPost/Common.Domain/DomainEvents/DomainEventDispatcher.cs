using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
	public class DomainEventDispatcher : IDomainEventDispatcher
	{
	    private readonly IPendingDomainEventProvider _pendingDomainEventProvider;
	    private readonly IDomainEventBus _domainEventBus;

        public DomainEventDispatcher(IDomainEventBus domainEventBus, IPendingDomainEventProvider pendingDomainEventProvider)
        {
            _domainEventBus = domainEventBus;
            _pendingDomainEventProvider = pendingDomainEventProvider;
        }

		public async Task DispatchEventsAsync()
		{
		    await Task.Run(() => Parallel.ForEach(_pendingDomainEventProvider.GetAll(),
		        message => { _domainEventBus.Publish(message); }));
        }
	}
}
