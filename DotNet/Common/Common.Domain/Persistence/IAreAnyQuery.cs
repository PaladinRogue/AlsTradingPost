using System;
using System.Linq.Expressions;

namespace Common.Domain.Persistence
{
    public interface IAreAnyQuery<T>
    {
        /// <summary>Returns whether an entity exists with the given predicate.</summary>
        /// <param name="predicate">A predicate to test an entity for against.</param>
        /// <returns>A boolean denoting if the entity exists.</returns>
        bool AreAny(Expression<Func<T, bool>> predicate);
    }
}
