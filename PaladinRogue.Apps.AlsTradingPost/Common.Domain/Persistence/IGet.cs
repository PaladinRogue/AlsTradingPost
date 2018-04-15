using System;
using System.Linq;

namespace Common.Domain.Persistence
{
    public interface IGet<out T>
    {
        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        IQueryable<T> Get(Predicate<T> predicate = null);

        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <param name="orderBy">The ordering function to be applied.</param>
        /// <param name="orderByAscending">The direction of the ordering.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        IOrderedQueryable<T> Get<TOrderByKey>(Predicate<T> predicate = null,
            Func<T, TOrderByKey> orderBy = null,
            bool orderByAscending = true);

        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <param name="orderBy">The ordering function to be applied.</param>
        /// <param name="orderByAscending">The direction of the ordering.</param>
        /// <param name="thenBy">The next ordering function to be applied.</param>
        /// <param name="thenByAscending">The direction of the next ordering.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        IOrderedQueryable<T> Get<TOrderByKey, TThenByKey>(Predicate<T> predicate = null,
            Func<T, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<T, TThenByKey> thenBy = null,
            bool thenByAscending = true);
    }
}
