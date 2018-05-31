namespace Common.Domain.Models.Interfaces
{
    public interface IOwnedAggregate : IEntity
    {
        IAggregateOwner GetOwner();
    }
}