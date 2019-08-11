using System;
using System.Threading.Tasks;
using PaladinRogue.Libray.Core.Domain.Exceptions;

namespace PaladinRogue.Libray.Core.Domain.Persistence
{
    public interface IGetByIdQuery<T>
    {
        /// <summary>Returns the only entity of a sequence that satisfies a specified condition or a null value if no such entity exists; this method throws an exception if more than one entity satisfies the condition.</summary>
        /// <param name="id">The id of the entity to return.</param>
        /// <returns>The single entity <typeparamref name="T"/>, or null if no such entity is found.</returns>
        /// <exception cref="DomainException">More than one entity is found with given <paramref name="id">id</paramref>.</exception>
        Task<T> GetByIdAsync(Guid id);
    }
}
