using System;

namespace Common.Authorisation.Manager
{
    public interface IAuthorisationManager
    {
        bool HasAccess(IAuthorisationContext authorisationContext);

        /// <param name="authorisationContext"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        void DemandAccess(IAuthorisationContext authorisationContext);
    }
}
