using System;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;
using PaladinRogue.Library.Core.Domain.Exceptions;

namespace PaladinRogue.Library.Core.Domain.Persistence
{
    public interface ICheckConcurrencyQuery<T>
    {
        /// <summary>Returns whether an entity exists with the given id and version.</summary>
        /// <param name="id">The id of the entity to check.</param>
        /// <param name="version">The version of the entity to check.</param>
        /// <returns>The entity which exists with id and version.</returns>
        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="NotFoundDomainException"></exception>
        Task<T> GetWithConcurrencyCheckAsync(Guid id, IConcurrencyVersion version);
    }
}
