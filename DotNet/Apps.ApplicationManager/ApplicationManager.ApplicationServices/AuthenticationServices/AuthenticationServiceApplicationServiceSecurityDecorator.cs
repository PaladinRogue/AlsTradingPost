using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using Common.Authorisation;
using Common.Authorisation.ApplicationServices;
using Common.Authorisation.Contexts;

namespace ApplicationManager.ApplicationServices.AuthenticationServices
{
    public class AuthenticationServiceApplicationServiceSecurityDecorator : IAuthenticationServiceApplicationService
    {
        private readonly ISecurityApplicationService _securityApplicationService;

        private readonly IAuthenticationServiceApplicationService _authenticationServiceApplicationService;

        public AuthenticationServiceApplicationServiceSecurityDecorator(
            ISecurityApplicationService securityApplicationService,
            IAuthenticationServiceApplicationService authenticationServiceApplicationService)
        {
            _securityApplicationService = securityApplicationService;
            _authenticationServiceApplicationService = authenticationServiceApplicationService;
        }

        public ClientCredentialAdto CreateClientCredential(CreateClientCredentialAdto createClientCredentialAdto)
        {
            return _securityApplicationService.Secure(() => _authenticationServiceApplicationService.CreateClientCredential(createClientCredentialAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Create));
        }
    }
}