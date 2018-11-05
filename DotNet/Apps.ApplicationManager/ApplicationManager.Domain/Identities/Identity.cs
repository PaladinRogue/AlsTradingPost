using ApplicationManager.Domain.Identities.Sessions;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.Identities
{
    public class Identity : VersionedEntity, IAggregateRoot
    {
        public Session Session { get; protected set; }
    }
}
