﻿using System;

namespace PaladinRogue.Library.Core.Domain.Aggregates
{
    public class AggregateOwner<T> : IAggregateOwner where T : IAggregateRoot
    {
        public AggregateOwner(Guid id)
        {
            Id = id;
        }

        public Type AggregateType => typeof(T);
        public Guid Id { get; }
    }
}
