using System.Collections.Generic;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventFactory
	{
		void Raise(IDomainEvent domainEventSubscriber);
		IEnumerable<IDomainEvent> GetAll();
	}
}
