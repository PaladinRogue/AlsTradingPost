namespace PaladinRogue.Libray.Core.Domain.Aggregates
{
    public interface IAggregateMember
    {
        IAggregateRoot AggregateRoot { get; }
    }
}