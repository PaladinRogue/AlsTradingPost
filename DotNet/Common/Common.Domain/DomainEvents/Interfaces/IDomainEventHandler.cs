using System.Threading.Tasks;

namespace Common.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventHandler<in T> where T : IDomainEvent
	{
		Task HandleAsync(T domainEvent);
	}
}
