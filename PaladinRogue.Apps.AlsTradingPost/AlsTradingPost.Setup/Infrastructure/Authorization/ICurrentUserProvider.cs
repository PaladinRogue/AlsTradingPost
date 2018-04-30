using System;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public interface ICurrentUserProvider
    {
        Guid Id { get; }
    }
}
