using System;
using System.Collections.Generic;

namespace AlsTradingPost.Persistence.Interfaces
{
    public interface IRepository<T>
    {
        IList<T> Get();
        T GetById(Guid id);
        void Add(T obj);
        void Update(T obj);
        void Delete(Guid id);
    }
}
