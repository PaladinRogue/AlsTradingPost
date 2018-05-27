using System;

namespace Common.Application.Authorisation
{
    public interface IAuthorisationManager
    {
        bool HasAccess(IAuthorisationRule authorisationRule, IAuthorisationContext authorisationContext);

        /// <param name="authorisationRule"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        void DemandAccess(IAuthorisationRule authorisationRule, IAuthorisationContext authorisationContext);
    }
}
