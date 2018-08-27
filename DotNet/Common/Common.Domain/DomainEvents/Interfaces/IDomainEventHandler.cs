namespace Common.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventHandler<in T> where T : IDomainEvent
	{
		void Handle(T domainEvent);
	}
}
