using System.Threading.Tasks;

namespace Common.Domain.DomainEvents.Interfaces
{
    public interface IDomainEventBus
    {
        Task PublishAsync<T>(T domainEvent) where T : IDomainEvent;
    }
}
