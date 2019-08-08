using System;
using System.Threading.Tasks;
using Authorisation.Application.Contexts;

namespace Authorisation.Application.Manager
{
    public interface IAuthorisationManager
    {
        Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext);

        /// <param name="authorisationContext"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Task DemandAccessAsync(IAuthorisationContext authorisationContext);
    }
}
