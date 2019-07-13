using ApplicationManager.Domain.Identities;
using Common.Domain.Aggregates;
using Common.Domain.Entities;

namespace ApplicationManager.Domain.Users
{
    public class User : VersionedEntity, IAggregateRoot
    {
        protected User()
        {
        }
        
        protected User(Identity identity)
        {
            Identity = identity;
        }

        internal static User Create(Identity identity)
        {
            return new User(identity);
        }

        public virtual Identity Identity { get; protected set; }
    }
}