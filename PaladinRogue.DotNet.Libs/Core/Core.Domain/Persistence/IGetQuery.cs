using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Common.Sorting;

namespace PaladinRogue.Library.Core.Domain.Persistence
{
    public interface IGetQuery<T>
    {
        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="sort">The ordering to be applied.</param>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        Task<IQueryable<T>> GetAsync(IList<SortBy> sort = null,
            Expression<Func<T, bool>> predicate = null);
    }
}
