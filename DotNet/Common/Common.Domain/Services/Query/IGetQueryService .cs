using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Domain.Models.Interfaces;
using Common.Resources.Sorting;

namespace Common.Domain.Services.Query
{
    public interface IGetQueryService<T> where T : IEntity
    {
        IEnumerable<T> Get(
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null);
    }
}
