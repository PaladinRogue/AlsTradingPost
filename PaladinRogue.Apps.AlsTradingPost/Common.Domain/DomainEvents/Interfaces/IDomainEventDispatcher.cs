using System.Threading.Tasks;

namespace Common.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventDispatcher
	{
		Task DispatchEventsAsync();
	}
}