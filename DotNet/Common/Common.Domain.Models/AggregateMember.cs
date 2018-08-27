using System;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Models
{
    public abstract class AggregateMember : IAggregateMember
    {
        protected AggregateMember()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
