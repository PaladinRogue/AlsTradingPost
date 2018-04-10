using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public abstract class DomainEventHandler<T, THandler> : IDomainEventHandler, IDomainEventHandler<T>
        where T : IDomainEvent
        where THandler : IDomainEventHandler<T>
    {
        private readonly IDomainEventBus _domainEventBus;

        protected DomainEventHandler(IDomainEventBus domainEventBus)
        {
            _domainEventBus = domainEventBus;
        }

        public abstract void Handle(T message);

        public void Register()
        {
            _domainEventBus.Subscribe<T, THandler>(Handle);
        }
    }
}
