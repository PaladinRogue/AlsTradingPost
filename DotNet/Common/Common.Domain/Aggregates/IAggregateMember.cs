namespace Common.Domain.Aggregates
{
    public interface IAggregateMember
    {
        IAggregateRoot AggregateRoot { get; }
    }
}