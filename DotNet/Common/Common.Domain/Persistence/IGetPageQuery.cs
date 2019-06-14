using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Resources.Sorting;

namespace Common.Domain.Persistence
{
    public interface IGetPageQuery<T>
    {
        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="pageSize">The page size of the entities to return.</param>
        /// <param name="pageOffset">The page offset of the entities to return.</param>
        /// <param name="totalResults">The total results of the entities.</param>
        /// <param name="sort">The ordering to be applied.</param>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        IQueryable<T> GetPage(int pageSize, int pageOffset, out int totalResults,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null);
    }
}
