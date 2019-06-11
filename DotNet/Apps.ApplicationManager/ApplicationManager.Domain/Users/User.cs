using ApplicationManager.Domain.Identities;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

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