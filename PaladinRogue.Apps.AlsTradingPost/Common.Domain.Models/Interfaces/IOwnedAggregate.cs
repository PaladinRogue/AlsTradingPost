namespace Common.Domain.Models.Interfaces
{
    public interface IOwnedAggregate
    {
        IAggregateOwner GetOwner();
    }
}