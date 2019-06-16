using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.Identities
{
    public abstract class AuthenticationIdentity : Entity, IAggregateMember
    {
        [Required]
        public virtual Identity Identity { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;
    }
}
