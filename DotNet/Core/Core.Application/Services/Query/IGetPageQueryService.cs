using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaladinRogue.Libray.Core.Common.Sorting;
using PaladinRogue.Libray.Core.Domain.Entities;
using PaladinRogue.Libray.Core.Domain.Models;
using PaladinRogue.Libray.Core.Domain.Pagination.Interfaces;
using PaladinRogue.Libray.Core.Domain.Sorting;

namespace PaladinRogue.Libray.Core.Application.Services.Query
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
