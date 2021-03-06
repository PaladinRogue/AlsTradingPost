﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Domain.Exceptions;

namespace PaladinRogue.Library.Core.Domain.Persistence
{
    public interface IGetSingleQuery<T>
    {
        /// <summary>Returns the only entity of a sequence that satisfies a specified predicate or a null value if no such entity exists; this method throws an exception if more than one entity satisfies the condition.</summary>
        /// <param name="predicate">A predicate to test an entity for against.</param>
        /// <returns>The single entity <typeparamref name="T"/>, or null if no such entity is found.</returns>
        /// <exception cref="GetSingleDomainException{T}">More than one entity is found which matches the given <paramref name="predicate">predicate</paramref>.</exception>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
    }
}
