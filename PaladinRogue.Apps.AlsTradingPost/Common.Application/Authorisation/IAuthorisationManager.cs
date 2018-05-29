using System;

namespace Common.Application.Authorisation
{
    public interface IAuthorisationManager
    {
        bool HasAccess(IAuthorisationContext authorisationContext);

        /// <param name="authorisationContext"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        void DemandAccess(IAuthorisationContext authorisationContext);
    }
}
