namespace Common.Domain.Models.Interfaces
{
    public interface IAggregateMember
    {
        IAggregateRoot AggregateRoot { get; }
    }
}