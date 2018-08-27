using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Domain.Models.Interfaces;
using Common.Domain.Pagination.Interfaces;
using Common.Resources.Sorting;

namespace Common.Domain.Services.Query
{
    public interface IGetPageQueryService<T> where T : IEntity
    {
        IEnumerable<T> GetPage(IPaginationDdto paginationDdto,
            out int totalResults,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null);
    }
}
