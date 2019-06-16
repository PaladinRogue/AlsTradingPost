using System;
using Common.Application.Exceptions;
using Common.Authorisation.Manager;

namespace Common.Authorisation
{
    public class DefaultSecurityApplicationService : ISecurityApplicationService
    {
        private readonly IAuthorisationManager _authorisationManager;

        public DefaultSecurityApplicationService(IAuthorisationManager authorisationManager)
        {
            _authorisationManager = authorisationManager;
        }

        public TOut Secure<TOut>(Func<TOut> function, IAuthorisationContext authorisationContext)
        {
            try
            {
                _authorisationManager.DemandAccess(authorisationContext);

                return function();
            }
            catch (UnauthorizedAccessException e)
            {
                throw new BusinessApplicationException(ExceptionType.Unauthorized, "You are not authorised to perform this action", e);
            }
        }
    }
}
