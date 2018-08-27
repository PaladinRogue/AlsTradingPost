using System;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Persistence
{
    public interface ICheckConcurrency
    {
        /// <summary>Returns whther an entity exists with the given id and version.</summary>
        /// <param name="id">The id of the entity to check.</param>
        /// <param name="version">The version of the entity to check.</param>
        /// <returns>A boolean denotig if the entity exists.</returns>
        bool CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
