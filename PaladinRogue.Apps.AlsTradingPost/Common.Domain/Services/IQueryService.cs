using System;

namespace Common.Domain.Services
{
    public interface IQueryService<out T>
    {
        T Get(Guid id);
    }
}
