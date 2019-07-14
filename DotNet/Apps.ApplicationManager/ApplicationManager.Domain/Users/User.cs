using System.Threading.Tasks;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Users.Events;
using Common.Domain.Aggregates;
using Common.Domain.DomainEvents;
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

        internal static async Task<User> Create(Identity identity)
        {
            User user = new User(identity);

            await DomainEvents.RaiseAsync(UserCreatedDomainEvent.Create(user));

            return user;
        }

        public virtual Identity Identity { get; protected set; }
    }
}