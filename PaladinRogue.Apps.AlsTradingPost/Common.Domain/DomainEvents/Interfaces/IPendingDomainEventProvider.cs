using Common.Resources.Interfaces;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IPendingDomainEventProvider : IProvider<IDomainEvent>
    {
    }
}
