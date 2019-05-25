using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.Identities.AuthenticationIdentities
{
    public abstract class AuthenticationIdentity : Entity, IAggregateMember
    {
        public abstract string Type { get; protected set; }
        
        public Identity Identity { get; protected set; }

        public IAggregateRoot AggregateRoot => Identity;
    }
}
