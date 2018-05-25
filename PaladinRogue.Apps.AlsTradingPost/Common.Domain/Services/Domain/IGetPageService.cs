using System;
using System.Linq.Expressions;
using Common.Domain.Pagination.Interfaces;

namespace Common.Domain.Services.Domain
{
    public interface IGetPageService<T, out TOut>
    {
        TOut GetPage(IPaginationDdto paginationDdto, Expression<Func<T, bool>> predicate = null,
            string orderBy = null,
            bool orderByAscending = true,
            string thenBy = null,
            bool? thenByAscending = null);
    }
}
