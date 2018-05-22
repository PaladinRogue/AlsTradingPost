namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventBus
    {
        void Publish(IDomainEvent domainEvent);

        void Subscribe<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>;

        void Unsubscribe<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>;
    }
}
