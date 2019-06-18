using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication.Models;
using Common.Application.Authentication;
using Common.Authorisation;

namespace ApplicationManager.ApplicationServices.Authentication
{
    public class AuthenticationApplicationServiceSecurityDecorator : IAuthenticationApplicationService
    {
        private readonly ISecurityApplicationService _securityApplicationService;

        private readonly IAuthenticationApplicationService _authenticationApplicationService;

        public AuthenticationApplicationServiceSecurityDecorator(
            ISecurityApplicationService securityApplicationService,
            IAuthenticationApplicationService authenticationApplicationService)
        {
            _securityApplicationService = securityApplicationService;
            _authenticationApplicationService = authenticationApplicationService;
        }

        public Task<JwtAdto> Password(PasswordAdto passwordAdto)
        {
            return _securityApplicationService.Secure(() => _authenticationApplicationService.Password(passwordAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Authenticate, AuthorisationAction.Create));
        }
    }
}