namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventFectory
	{
		void Raise(IDomainEvent domainEventSubscriber);
    }
}
