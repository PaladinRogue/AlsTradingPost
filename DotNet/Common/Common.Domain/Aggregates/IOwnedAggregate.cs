using Common.Domain.Entities;

namespace Common.Domain.Aggregates
{
    public interface IOwnedAggregate : IEntity
    {
        IAggregateOwner GetOwner();
    }
}