namespace Common.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventDispatcher
	{
	    void DispatchEvent<T>(T domainEvent) where T : IDomainEvent;
	}
}