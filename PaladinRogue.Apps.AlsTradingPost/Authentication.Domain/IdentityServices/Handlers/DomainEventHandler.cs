using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;

namespace Authentication.Domain.IdentityServices.Handlers
{
	public class DomainEvent : IDomainEvent
	{
		public string Test { get; set; }
	}

	public class CustomDomainEventHandler : IDomainEventHandler, IDomainEventHandler<DomainEvent>
	{
		public void Register()
		{
			DomainEventHandlerFactory.Register<DomainEvent>(Handle);
		}

		public void Handle(DomainEvent domainEvent)
		{
			var temp = domainEvent.Test;
		}
	}
}
