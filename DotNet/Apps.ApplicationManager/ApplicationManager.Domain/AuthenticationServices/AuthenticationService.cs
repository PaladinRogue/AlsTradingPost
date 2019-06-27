using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Models;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public abstract class AuthenticationService : VersionedEntity, IAggregateRoot
    {
    }
}
