using System;
using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Domain.Persistence
{
    public interface IDeleteCommand
    {
        /// <summary>Deletes an entity of type <typeparamref name="T"/>.</summary>
        /// <param name="id">The id of the entity to delete.</param>
        /// <exception cref="DeleteDomainException">Failed to delete <paramref name="id">entity</paramref>.</exception>
        /// <exception cref="ConcurrencyDomainException">Concurrency check has failed for given <paramref name="id"/>entity.</exception>
        Task DeleteAsync(Guid id);
    }
}
