using System;

namespace Common.Domain.Services.Interfaces
{
    public interface IQueryService<out T>
    {
        T GetById(Guid id);
    }
}
