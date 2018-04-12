using System;

namespace AlsTradingPost.Resources.Providers.Interfaces
{
    public interface ICurrentUserProvider
    {
        Guid Id { get; }
    }
}
