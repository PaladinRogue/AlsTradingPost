using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventDispatcher
	{
	    Task DispatchEventAsync<T>(T domainEvent) where T : IDomainEvent;
	}
}