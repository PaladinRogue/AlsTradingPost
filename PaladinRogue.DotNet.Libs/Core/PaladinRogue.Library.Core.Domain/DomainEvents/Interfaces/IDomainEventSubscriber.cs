using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventSubscriber<in T> where T : IDomainEvent
	{
		Task ExecuteAsync(T domainEvent);
	}
}
