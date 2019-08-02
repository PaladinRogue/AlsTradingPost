using Common.Domain.Aggregates;
using Common.Domain.Entities;

namespace Authentication.Domain.AuthenticationServices
{
    public abstract class AuthenticationService : VersionedEntity, IAggregateRoot
    {
    }
}
