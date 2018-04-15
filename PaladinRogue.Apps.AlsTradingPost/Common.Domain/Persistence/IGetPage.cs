using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.Domain.Persistence
{
    public interface IGetPage<T>
    {
        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="pageSize">The page size of the entities to return.</param>
        /// <param name="pageOffset">The page offset of the entities to return.</param>
        /// <param name="totalResults">The total results of the entities.</param>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        IEnumerable<T> GetPage(int pageSize, int pageOffset, out int totalResults,
            Predicate<T> predicate = null);

        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="pageSize">The page size of the entities to return.</param>
        /// <param name="pageOffset">The page offset of the entities to return.</param>
        /// <param name="totalResults">The total results of the entities.</param>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <param name="orderBy">The ordering function to be applied.</param>
        /// <param name="orderByAscending">The direction of the ordering.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        IEnumerable<T> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults,
            Predicate<T> predicate = null,
            Expression<Func<T, TOrderByKey>> orderBy = null,
            bool orderByAscending = true);

        /// <summary>Gets all entities of type <typeparamref name="T"/> which match predicate <paramref name="predicate"/>.</summary>
        /// <param name="pageSize">The page size of the entities to return.</param>
        /// <param name="pageOffset">The page offset of the entities to return.</param>
        /// <param name="totalResults">The total results of the entities.</param>
        /// <param name="predicate">The predicate of the entities to return.</param>
        /// <param name="orderBy">The ordering function to be applied.</param>
        /// <param name="orderByAscending">The direction of the ordering.</param>
        /// <param name="thenBy">The next ordering function to be applied.</param>
        /// <param name="thenByAscending">The direction of the next ordering.</param>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        IEnumerable<T> GetPage<TOrderByKey, TThenByKey>(int pageSize, int pageOffset, out int totalResults,
            Predicate<T> predicate = null,
            Expression<Func<T, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<T, TThenByKey>> thenBy = null,
            bool thenByAscending = true);
    }
}
