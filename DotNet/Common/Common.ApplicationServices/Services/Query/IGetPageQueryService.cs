using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Domain.Entities;
using Common.Domain.Models;
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
        /// <param name="sort"></param>
        /// <param name="predicate"></param>
        /// <exception cref="PropertyNotSortableException"></exception>
        /// <returns></returns>
        Task<IPagedResult<T>> GetPageAsync(IPaginationDdto paginationDdto,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null);
    }
}
