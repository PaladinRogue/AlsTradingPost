using System;

namespace Common.Setup.Infrastructure.Authorization
{
    public interface ICurrentIdentityProvider
    {
        Guid Id { get; }
    }
}
