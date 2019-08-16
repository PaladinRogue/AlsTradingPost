using System;

namespace PaladinRogue.Library.Core.Domain.Aggregates
{
    public interface IAggregateOwner
    {
        Type AggregateType { get; }

        Guid Id { get; }
    }
}