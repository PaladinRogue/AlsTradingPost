using System.Collections.Generic;
using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEventHandlers : IDomainEventHandlers
	{
		private readonly IEnumerable<IDomainEventHandler> _handlers;

		public DomainEventHandlers(IEnumerable<IDomainEventHandler> handlers)
		{
			_handlers = handlers;
		}

		public void Initialise()
		{
			foreach (IDomainEventHandler domainEventHandler in _handlers)
			{
				domainEventHandler.Register();
			}
		}
	}
}
