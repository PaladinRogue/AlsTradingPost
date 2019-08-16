using PaladinRogue.Library.Core.Domain.Entities;

namespace PaladinRogue.Library.Core.Domain.Aggregates
{
    public interface IOwnedAggregate : IEntity
    {
        IAggregateOwner GetOwner();
    }
}