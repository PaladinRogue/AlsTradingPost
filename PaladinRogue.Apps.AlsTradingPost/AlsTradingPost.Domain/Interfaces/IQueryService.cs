using System;

namespace AlsTradingPost.Domain.Interfaces
{
    public interface IQueryService<out T>
    {
        T Get(Guid id);
    }
}
