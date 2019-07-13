using Common.Domain.Aggregates;
using Common.Domain.Entities;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public abstract class AuthenticationService : VersionedEntity, IAggregateRoot
    {
    }
}
