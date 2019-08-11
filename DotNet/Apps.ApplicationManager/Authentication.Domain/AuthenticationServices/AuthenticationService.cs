using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices
{
    public abstract class AuthenticationService : VersionedEntity, IAggregateRoot
    {
    }
}
