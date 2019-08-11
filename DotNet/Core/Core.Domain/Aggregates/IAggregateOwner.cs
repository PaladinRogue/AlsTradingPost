using System;

namespace PaladinRogue.Libray.Core.Domain.Aggregates
{
    public interface IAggregateOwner
    {
        Type AggregateType { get; }

        Guid Id { get; }
    }
}