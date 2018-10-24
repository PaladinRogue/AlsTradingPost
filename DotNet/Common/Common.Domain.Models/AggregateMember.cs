using System;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Models
{
    public abstract class AggregateMember : IEntity, IAggregateMember
    {
        protected AggregateMember()
        {
            Id = Guid.NewGuid();
        }

        public abstract IAggregateRoot AggregateRoot { get; }

        public Guid Id { get; set; }
    }
}
