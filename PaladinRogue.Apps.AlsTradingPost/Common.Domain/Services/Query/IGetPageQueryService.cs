using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Domain.Models.Interfaces;
using Common.Domain.Pagination.Interfaces;

namespace Common.Domain.Services.Query
{
    public interface IGetPageQueryService<T> where T : IEntity
    {
        IEnumerable<T> GetPage(IPaginationDdto paginationDdto,
            out int totalResults,
            Expression<Func<T, bool>> predicate = null,
            string orderBy = null,
            bool orderByAscending = true,
            string thenBy = null,
            bool? thenByAscending = null);
    }
}
