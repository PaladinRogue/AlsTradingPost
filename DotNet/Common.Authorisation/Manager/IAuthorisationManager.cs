using System;
using System.Threading.Tasks;
using Common.Authorisation.Contexts;

namespace Common.Authorisation.Manager
{
    public interface IAuthorisationManager
    {
        Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext);

        /// <param name="authorisationContext"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Task DemandAccessAsync(IAuthorisationContext authorisationContext);
    }
}
