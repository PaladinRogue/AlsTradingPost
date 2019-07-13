using System.ComponentModel.DataAnnotations;
using Common.Domain.Aggregates;
using Common.Domain.Entities;

namespace ApplicationManager.Domain.Identities
{
    public abstract class AuthenticationIdentity : Entity, IAggregateMember
    {
        [Required]
        public virtual Identity Identity { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;
    }
}
