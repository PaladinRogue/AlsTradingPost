using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventBus
    {
        Task PublishAsync<T>(T domainEvent) where T : IDomainEvent;
    }
}
