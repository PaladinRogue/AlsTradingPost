using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Libray.Core.Domain.Aggregates
{
    public interface IOwnedAggregate : IEntity
    {
        IAggregateOwner GetOwner();
    }
}