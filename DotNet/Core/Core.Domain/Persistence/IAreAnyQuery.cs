using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Domain.Persistence
{
    public interface IAreAnyQuery<T>
    {
        /// <summary>Returns whether an entity exists with the given predicate.</summary>
        /// <param name="predicate">A predicate to test an entity for against.</param>
        /// <returns>A boolean denoting if the entity exists.</returns>
        Task<bool> AreAnyAsync(Expression<Func<T, bool>> predicate);
    }
}
