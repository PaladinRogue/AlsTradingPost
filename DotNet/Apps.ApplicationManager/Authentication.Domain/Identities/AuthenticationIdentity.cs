using System.ComponentModel.DataAnnotations;
using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Authentication.Domain.Identities
{
    public abstract class AuthenticationIdentity : Entity, IAggregateMember
    {
        [Required]
        public virtual Identity Identity { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;
    }
}
