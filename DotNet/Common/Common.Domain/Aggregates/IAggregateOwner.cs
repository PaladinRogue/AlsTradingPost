using System;

namespace Common.Domain.Aggregates
{
    public interface IAggregateOwner
    {
        Type AggregateType { get; }

        Guid Id { get; }
    }
}