using System;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Authorisation
{
    public interface ICurrentUserProvider
    {
        bool IsAuthenticated { get; }

        Guid? Id { get; }
    }
}
