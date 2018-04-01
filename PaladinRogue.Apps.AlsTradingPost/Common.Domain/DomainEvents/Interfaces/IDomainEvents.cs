using System.Collections.Generic;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEvents
	{
		void Raise(IDomainEvent domainEventSubscriber);
		IEnumerable<IDomainEvent> GetAll();
	}
}
