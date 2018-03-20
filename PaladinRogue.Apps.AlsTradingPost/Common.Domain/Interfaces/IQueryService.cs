using System;

namespace Common.Domain.Interfaces
{
    public interface IQueryService<out T>
    {
        T Get(Guid id);
    }
}
