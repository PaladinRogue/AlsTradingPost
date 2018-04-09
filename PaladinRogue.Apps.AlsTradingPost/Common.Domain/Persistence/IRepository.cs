using System;
using System.Collections.Generic;
using Common.Domain.Exceptions;

namespace Common.Domain.Persistence
{
    public interface IRepository<T>
    {
        /// <summary>Gets all entities of type <typeparamref name="T"/>.</summary>
        /// <returns>Returns an enumerable entities of type <typeparamref name="T"/></returns>
        IEnumerable<T> Get();
        /// <summary>Returns the only entity of a sequence that satisfies a specified condition or a null value if no such entity exists; this method throws an exception if more than one entity satisfies the condition.</summary>
        /// <param name="id">The id of the entity to return.</param>
        /// <returns>The single entity <typeparamref name="T"/>, or null if no such entity is found.</returns>
        /// <exception cref="DomainException">More than one entity is found with given <paramref name="id">id</paramref>.</exception>
        T GetById(Guid id);
        /// <summary>Returns the only entity of a sequence that satisfies a specified predicate or a null value if no such entity exists; this method throws an exception if more than one entity satisfies the condition.</summary>
        /// <param name="predicate">A predicate to test an entity for against.</param>
        /// <returns>The single entity <typeparamref name="T"/>, or null if no such entity is found.</returns>
        /// <exception cref="DomainException">More than one entity is found which matches the given <paramref name="predicate">predicate</paramref>.</exception>
        T GetSingle(Predicate<T> predicate);
        /// <summary>Adds an entity of type <typeparamref name="T"/>.</summary>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="UpdateDomainException">Failed to add <paramref name="entity">entity</paramref>.</exception>
        /// <exception cref="ConcurrencyDomainException">Concurrency check has failed for given <typeparamref name="T">entity</typeparamref>.</exception>
        void Add(T entity);
        /// <summary>Updates an entity of type <typeparamref name="T"/>.</summary>
        /// <param name="entity">The entity to update.</param>
        /// <exception cref="UpdateDomainException">Failed to update <paramref name="entity">entity</paramref>.</exception>
        /// <exception cref="ConcurrencyDomainException">Concurrency check has failed for given <typeparamref name="T">entity</typeparamref>.</exception>
        void Update(T entity);
        /// <summary>Deletes an entity of type <typeparamref name="T"/>.</summary>
        /// <param name="id">The id of the entity to delete.</param>
        /// <exception cref="UpdateDomainException">Failed to delete <paramref name="id">entity</paramref>.</exception>
        /// <exception cref="ConcurrencyDomainException">Concurrency check has failed for given <typeparamref name="id">entity</typeparamref>.</exception>
        void Delete(Guid id);
    }
}
