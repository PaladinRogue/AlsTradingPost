using System.Threading.Tasks;
using PaladinRogue.Library.Core.Domain.Exceptions;

namespace PaladinRogue.Library.Core.Domain.Persistence
{
    public interface IAddCommand<in T>
    {
        /// <summary>Adds an entity of type <typeparamref name="T"/>.</summary>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="CreateDomainException">Failed to add <paramref name="entity">entity</paramref>.</exception>
        /// <exception cref="ConcurrencyDomainException">Concurrency check has failed for given <typeparamref name="T">entity</typeparamref>.</exception>
        Task AddAsync(T entity);
    }
}
