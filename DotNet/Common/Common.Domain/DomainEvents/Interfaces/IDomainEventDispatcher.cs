using System.Threading.Tasks;

namespace Common.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventDispatcher
	{
	    Task DispatchEventAsync<T>(T domainEvent) where T : IDomainEvent;
	}
}