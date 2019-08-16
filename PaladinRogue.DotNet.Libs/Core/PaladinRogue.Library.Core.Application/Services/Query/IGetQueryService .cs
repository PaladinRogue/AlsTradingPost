using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Common.Sorting;
using PaladinRogue.Library.Core.Domain.Entities;
using PaladinRogue.Library.Core.Domain.Sorting;

namespace PaladinRogue.Library.Core.Application.Services.Query
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
