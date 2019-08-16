using System.Threading.Tasks;
using PaladinRogue.Library.Core.Domain.Exceptions;

namespace PaladinRogue.Library.Core.Domain.Persistence
{
    public interface IUpdateCommand<in T>
    {
        /// <summary>Updates an entity of type <typeparamref name="T"/>.</summary>
        /// <param name="entity">The entity to update.</param>
        /// <exception cref="UpdateDomainException">Failed to update <paramref name="entity">entity</paramref>.</exception>
        /// <exception cref="ConcurrencyDomainException">Concurrency check has failed for given <typeparamref name="T">entity</typeparamref>.</exception>
        Task UpdateAsync(T entity);
    }
}
