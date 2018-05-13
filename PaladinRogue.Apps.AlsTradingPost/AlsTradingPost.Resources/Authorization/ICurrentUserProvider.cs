using System;

namespace AlsTradingPost.Resources.Authorization
{
    public interface ICurrentUserProvider
    {
        Guid Id { get; }
    }
}
