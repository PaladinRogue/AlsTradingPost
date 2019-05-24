using System;

namespace Common.Setup.Infrastructure.Authorisation
{
    public interface ICurrentIdentityProvider
    {
        Guid Id { get; }
    }
}
