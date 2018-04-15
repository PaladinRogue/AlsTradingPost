using System;
using Common.Domain.Pagination.Interfaces;

namespace Common.Domain.Services.Interfaces
{
    public interface IPagedSummaryQueryService<T, out TOut>
    {
        TOut GetPage(IPaginationDdto paginationDdto, Predicate<T> predicate = null,
            string orderBy = null,
            bool orderByAscending = true,
            string thenBy = null,
            bool thenByAscending = true);
    }
}
