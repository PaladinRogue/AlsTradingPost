namespace Common.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventDispatcher
	{
		void DispatchEvent(IDomainEvent domainEvent);
	}
}