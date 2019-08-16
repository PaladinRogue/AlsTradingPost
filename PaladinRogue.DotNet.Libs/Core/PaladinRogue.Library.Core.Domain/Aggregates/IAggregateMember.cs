namespace PaladinRogue.Library.Core.Domain.Aggregates
{
    public interface IAggregateMember
    {
        IAggregateRoot AggregateRoot { get; }
    }
}