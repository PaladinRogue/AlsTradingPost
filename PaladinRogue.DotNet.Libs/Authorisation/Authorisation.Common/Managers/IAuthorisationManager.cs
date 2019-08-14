using System;
using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common.Contexts;

namespace PaladinRogue.Library.Authorisation.Common.Managers
{
    public interface IAuthorisationManager
    {
        Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext);

        /// <param name="authorisationContext"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Task DemandAccessAsync(IAuthorisationContext authorisationContext);
    }
}
