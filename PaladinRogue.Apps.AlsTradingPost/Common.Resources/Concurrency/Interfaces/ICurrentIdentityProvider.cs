using System;

namespace Common.Resources.Concurrency.Interfaces
{
    public interface ICurrentIdentityProvider
    {
        Guid Id { get; }
    }
}
