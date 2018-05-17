using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.Domain.Services.Interfaces
{
    public interface IGetQueryService<T, TOut>
    {
        IList<TOut> Get<TOrderByKey, TThenByKey>(Predicate<T> predicate = null,
            Expression<Func<T, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<T, TThenByKey>> thenBy = null,
            bool? thenByAscending = null);
    }
}
