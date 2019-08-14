using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Users.Events;
using PaladinRogue.Library.Core.Domain.Aggregates;
using PaladinRogue.Library.Core.Domain.DomainEvents;
using PaladinRogue.Library.Core.Domain.Entities;

namespace PaladinRogue.Authentication.Domain.Users
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