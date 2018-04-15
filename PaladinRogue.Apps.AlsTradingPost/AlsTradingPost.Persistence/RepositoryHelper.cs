using System;
using System.Collections.Generic;
using System.Linq;

namespace AlsTradingPost.Persistence
{
    public static class RepositoryHelper
    {
        public static IQueryable<T> Filter<T>(IQueryable<T> results, Predicate<T> predicate)
        {
            if (predicate != null)
            {
                results = results.Where(i => predicate(i));
            }

            return results;
        }

        public static IOrderedQueryable<T> OrderBy<T, TOrderByKey>(IQueryable<T> results,
            Func<T, TOrderByKey> orderBy, bool orderByAscending)
        {
            if (orderBy != null)
            {
                results = orderByAscending ? results.OrderBy(x => orderBy(x)) : results.OrderByDescending(x => orderBy(x));
            }

            return (IOrderedQueryable<T>)results;
        }

        public static IOrderedQueryable<T> ThenBy<T, TThenByKey>(IOrderedQueryable<T> results,
            Func<T, TThenByKey> thenBy, bool thenByAscending)
        {
            if (thenBy != null)
            {
                results = thenByAscending ? results.ThenBy(x => thenBy(x)) : results.ThenByDescending(x => thenBy(x));
            }

            return results;
        }
    }
}
