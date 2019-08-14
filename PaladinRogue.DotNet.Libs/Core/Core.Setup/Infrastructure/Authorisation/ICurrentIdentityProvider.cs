using System;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Authorisation
{
    public interface ICurrentIdentityProvider
    {
        bool IsAuthenticated { get; }

        Guid Id { get; }
    }
}
