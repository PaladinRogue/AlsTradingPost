using System;

namespace Common.Resources.Providers.Interfaces
{
    public interface ICurrentIdentityProvider
    {
        Guid Id { get; }
    }
}
