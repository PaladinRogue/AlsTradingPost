using System.Threading.Tasks;
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

		public async Task DispatchEventsAsync()
		{
			foreach (IDomainEvent domainEvent in _domainEvents.GetAll())
			{
				await Task.Run(() => Parallel.ForEach(DomainEventHandlerFactory.GetAllOfType(domainEvent.GetType()),
					domainEventHandler => { domainEventHandler.DynamicInvoke(domainEvent); }));
			}
		}
	}
}
