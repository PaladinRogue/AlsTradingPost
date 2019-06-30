using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Resources.Sorting;

namespace Common.Domain.Persistence
{
    public interface IGetQuery<T>
    {
        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="sort">The ordering to be applied.</param>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        IQueryable<T> Get(IList<SortBy> sort = null,
            Expression<Func<T, bool>> predicate = null);
    }
}
