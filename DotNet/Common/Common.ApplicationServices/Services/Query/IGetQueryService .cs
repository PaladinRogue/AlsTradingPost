using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Domain.Entities;
using Common.Domain.Sorting;
using Common.Resources.Sorting;

namespace Common.ApplicationServices.Services.Query
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
        IEnumerable<T> Get(
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null);
    }
}
