using System;

namespace Common.Domain.Models.Interfaces
{
    public interface IAggregateOwner
    {
        Type AggregateType { get; }

        Guid Id { get; }
    }
}