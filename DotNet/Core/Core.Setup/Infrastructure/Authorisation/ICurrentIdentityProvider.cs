using System;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Authorisation
{
    public interface ICurrentIdentityProvider
    {
        bool IsAuthenticated { get; }

        Guid Id { get; }
    }
}
