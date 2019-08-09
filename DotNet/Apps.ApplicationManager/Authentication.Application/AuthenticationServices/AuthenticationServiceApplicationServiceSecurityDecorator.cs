using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Application.AuthenticationServices.Models;
using Authorisation.Application;
using Authorisation.Application.ApplicationServices;
using Authorisation.Application.Contexts;

namespace Authentication.Application.AuthenticationServices
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

        public Task<IEnumerable<AuthenticationServiceAdto>> GetAuthenticationServicesAsync(GetAuthenticationServicesAdto getAuthenticationServicesAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationServiceApplicationService.GetAuthenticationServicesAsync(getAuthenticationServicesAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Search));
        }

        public Task<IEnumerable<AuthenticationServiceTypeAdto>> GetAuthenticationServiceTypes()
        {
            return _securityApplicationService.SecureAsync(() => _authenticationServiceApplicationService.GetAuthenticationServiceTypes(),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Create));
        }

        public Task DeleteClientCredentialAsync(DeleteClientCredentialAdto deleteClientCredentialAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationServiceApplicationService.DeleteClientCredentialAsync(deleteClientCredentialAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Delete));
        }
    }
}