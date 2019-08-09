using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Domain.Entities;
using Common.Domain.Sorting;
using Common.Resources.Sorting;

namespace Common.Application.Services.Query
{
    public interface IGetQueryService<T> where T : IEntity
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="predicate"></param>
        /// <exception cref="PropertyNotSortableException"></exception>
        /// <returns></returns>
        Task<IQueryable<T>> GetAsync(IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null);
    }
}
