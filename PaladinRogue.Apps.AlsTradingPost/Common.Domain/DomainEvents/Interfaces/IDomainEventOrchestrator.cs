using System.Collections.Generic;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventOrchestrator
    {
	    IList<IDomainEvent> DomainEvents { get; set; }

	    void DispatchEvents();
    }
}
