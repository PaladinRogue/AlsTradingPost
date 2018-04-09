using System;
using System.Collections.Generic;

namespace Common.Domain.Persistence
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
