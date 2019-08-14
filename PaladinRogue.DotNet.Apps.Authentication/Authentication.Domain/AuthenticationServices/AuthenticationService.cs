using PaladinRogue.Library.Core.Domain.Aggregates;
using PaladinRogue.Library.Core.Domain.Entities;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices
{
    public abstract class AuthenticationService : VersionedEntity, IAggregateRoot
    {
    }
}
