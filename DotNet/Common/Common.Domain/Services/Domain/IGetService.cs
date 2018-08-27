using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Resources.Sorting;

namespace Common.Domain.Services.Domain
{
    public interface IGetService<T, out TOut>
    {
        IEnumerable<TOut> Get(IList<SortBy> sort, Expression<Func<T, bool>> predicate = null);
    }
}
