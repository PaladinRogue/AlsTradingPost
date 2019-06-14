using System;
using System.Linq.Expressions;
using Common.Domain.Exceptions;

namespace Common.Domain.Persistence
{
    public interface IGetSingleQuery<T>
    {
        /// <summary>Returns the only entity of a sequence that satisfies a specified predicate or a null value if no such entity exists; this method throws an exception if more than one entity satisfies the condition.</summary>
        /// <param name="predicate">A predicate to test an entity for against.</param>
        /// <returns>The single entity <typeparamref name="T"/>, or null if no such entity is found.</returns>
        /// <exception cref="DomainException">More than one entity is found which matches the given <paramref name="predicate">predicate</paramref>.</exception>
        T GetSingle(Expression<Func<T, bool>> predicate);
    }
}
