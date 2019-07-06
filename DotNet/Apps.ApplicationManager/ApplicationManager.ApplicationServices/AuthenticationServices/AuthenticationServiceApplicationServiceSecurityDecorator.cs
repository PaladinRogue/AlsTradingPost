using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<IEnumerable<AuthenticationServiceAdto>> GetAuthenticationServicesAsync()
        {
            return _securityApplicationService.SecureAsync(() => _authenticationServiceApplicationService.GetAuthenticationServicesAsync(),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Search));
        }

        public Task<ClientCredentialAdto> CreateClientCredential(CreateClientCredentialAdto createClientCredentialAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationServiceApplicationService.CreateClientCredential(createClientCredentialAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Create));
        }

        public Task<ClientCredentialAdto> GetClientCredentialAsync(GetClientCredentialAdto getClientCredentialAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationServiceApplicationService.GetClientCredentialAsync(getClientCredentialAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Get));
        }

        public Task<ClientCredentialAdto> ChangeClientCredentialAsync(ChangeClientCredentialAdto changeClientCredentialAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationServiceApplicationService.ChangeClientCredentialAsync(changeClientCredentialAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Get));
        }
    }
}