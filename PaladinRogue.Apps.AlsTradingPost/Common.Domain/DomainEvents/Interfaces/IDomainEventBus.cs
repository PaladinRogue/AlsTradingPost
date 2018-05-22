namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventBus
    {
        void Publish<T>(T domainEvent) where T : IDomainEvent;
    }
}
