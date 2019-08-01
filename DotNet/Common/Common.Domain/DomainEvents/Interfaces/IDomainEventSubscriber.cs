using System.Threading.Tasks;

namespace Common.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventSubscriber<in T> where T : IDomainEvent
	{
		Task ExecuteAsync(T domainEvent);
	}
}
