using System;
using System.Linq.Expressions;
using Common.Domain.Pagination.Interfaces;

namespace Common.Domain.Services.Interfaces
{
    public interface IPagedSummaryQueryService<T, out TOut>
    {
        TOut GetPage(IPaginationDdto paginationDdto, Predicate<T> predicate = null);

        TOut GetPage<TOrderByKey>(IPaginationDdto paginationDdto, Predicate<T> predicate = null,
            Expression<Func<T, TOrderByKey>> orderBy = null,
            bool orderByAscending = true);

        TOut GetPage<TOrderByKey, TThenByKey>(IPaginationDdto paginationDdto, Predicate<T> predicate = null,
            Expression<Func<T, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<T, TThenByKey>> thenBy = null,
            bool thenByAscending = true);
    }
}
