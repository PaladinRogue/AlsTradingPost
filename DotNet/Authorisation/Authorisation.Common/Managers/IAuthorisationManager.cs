using System;
using System.Threading.Tasks;
using PaladinRogue.Libray.Authorisation.Common.Contexts;

namespace PaladinRogue.Libray.Authorisation.Common.Managers
{
    public interface IAuthorisationManager
    {
        Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext);

        /// <param name="authorisationContext"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Task DemandAccessAsync(IAuthorisationContext authorisationContext);
    }
}
