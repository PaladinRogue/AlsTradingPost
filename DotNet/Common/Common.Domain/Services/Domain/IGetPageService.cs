using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Domain.Pagination.Interfaces;
using Common.Resources.Sorting;

namespace Common.Domain.Services.Domain
{
    public interface IGetPageService<T, out TOut>
    {
        TOut GetPage(IPaginationDdto paginationDdto, IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null);
    }
}
