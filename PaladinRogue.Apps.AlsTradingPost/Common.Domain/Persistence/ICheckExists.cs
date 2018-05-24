using System;

namespace Common.Domain.Persistence
{
    public interface ICheckExists
    {
        /// <summary>Returns whther an entity exists with the given id.</summary>
        /// <param name="id">The id of the entity to check.</param>
        /// <returns>A boolean denotig if the entity exists.</returns>
        bool CheckExists(Guid id);
    }
}
