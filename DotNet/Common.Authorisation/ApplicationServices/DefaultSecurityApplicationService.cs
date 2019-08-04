using System;
using System.Threading.Tasks;
using Common.ApplicationServices.Exceptions;
using Common.Authorisation.Contexts;
using Common.Authorisation.Manager;

namespace Common.Authorisation.ApplicationServices
{
    public class DefaultSecurityApplicationService : ISecurityApplicationService
    {
        private readonly IAuthorisationManager _authorisationManager;

        public DefaultSecurityApplicationService(IAuthorisationManager authorisationManager)
        {
            _authorisationManager = authorisationManager;
        }

        public async Task<TOut> SecureAsync<TOut>(Func<Task<TOut>> function,
            IAuthorisationContext authorisationContext)
        {
            try
            {
                await _authorisationManager.DemandAccessAsync(authorisationContext);

                return await function();
            }
            catch (UnauthorizedAccessException e)
            {
                throw new BusinessApplicationException(ExceptionType.Unauthorized, "You are not authorised to perform this action", e);
            }
        }

        public async Task SecureAsync(Func<Task> function,
            IAuthorisationContext authorisationContext)
        {
            try
            {
                await _authorisationManager.DemandAccessAsync(authorisationContext);

                await function();
            }
            catch (UnauthorizedAccessException e)
            {
                throw new BusinessApplicationException(ExceptionType.Unauthorized, "You are not authorised to perform this action", e);
            }
        }
    }
}
