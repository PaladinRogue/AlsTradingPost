using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Common.Sorting;
using PaladinRogue.Library.Core.Domain.Models;

namespace PaladinRogue.Library.Core.Domain.Persistence
{
    public interface IGetPageQuery<T>
    {
        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="pageSize">The page size of the entities to return.</param>
        /// <param name="pageOffset">The page offset of the entities to return.</param>
        /// <param name="sort">The ordering to be applied.</param>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        Task<IPagedResult<T>> GetPageAsync(int pageSize, int pageOffset, IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null);
    }
}
