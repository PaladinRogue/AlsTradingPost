using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventSubscriber<in T> where T : IDomainEvent
	{
		Task ExecuteAsync(T domainEvent);
	}
}
