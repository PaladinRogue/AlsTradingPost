using Common.Resources.Interfaces;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IPendingDomainEventContainer : IContainer<IDomainEvent>
    {
    }
}
