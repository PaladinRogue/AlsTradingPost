using System;
using Common.Application.Exceptions;

namespace Common.Application.Authorisation
{
    public class DefaultSecurityApplicationService : ISecurityApplicationService
    {
        private readonly IAuthorisationService _authorisationService;

        public DefaultSecurityApplicationService(IAuthorisationService authorisationService)
        {
            _authorisationService = authorisationService;
        }

        public TOut Secure<TOut>(Func<TOut> function, IAuthorisationRule authorisationRule)
        {
            try
            {
                _authorisationService.DemandAccess(authorisationRule);

                return function();
            }
            catch (AuthorisationDeniedException e)
            {
                throw new BusinessApplicationException(ExceptionType.Unauthorized, e);
            }
        }
    }
}
