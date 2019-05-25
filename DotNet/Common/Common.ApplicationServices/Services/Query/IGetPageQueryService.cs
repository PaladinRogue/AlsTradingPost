using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Domain.Models.Interfaces;
using Common.Domain.Pagination.Interfaces;
using Common.Domain.Sorting;
using Common.Resources.Sorting;

namespace Common.ApplicationServices.Services.Query
{
    public interface IGetPageQueryService<T> where T : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginationDdto"></param>
        /// <param name="totalResults"></param>
        /// <param name="sort"></param>
        /// <param name="predicate"></param>
        /// <exception cref="PropertyNotSortableException"></exception>
        /// <returns></returns>
        IEnumerable<T> GetPage(IPaginationDdto paginationDdto,
            out int totalResults,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null);
    }
}
