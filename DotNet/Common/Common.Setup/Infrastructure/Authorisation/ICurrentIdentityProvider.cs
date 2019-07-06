using System;

namespace Common.Setup.Infrastructure.Authorisation
{
    public interface ICurrentIdentityProvider
    {
        bool IsAuthenticated { get; }

        Guid Id { get; }
    }
}
