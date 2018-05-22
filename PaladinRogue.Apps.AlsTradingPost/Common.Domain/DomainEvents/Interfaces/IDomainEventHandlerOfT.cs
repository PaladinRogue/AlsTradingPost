namespace Common.Domain.DomainEvents.Interfaces
{
	public interface IDomainEventHandler<in T>: IDomainEventHandler where T : IDomainEvent
	{
		void Handle(T domainEvent);
	}
}
