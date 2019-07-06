using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication.Models;
using Common.ApplicationServices.Authentication;
using Common.Authorisation;
using Common.Authorisation.ApplicationServices;
using Common.Authorisation.Contexts;

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

        public Task<JwtAdto> PasswordAsync(PasswordAdto passwordAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationApplicationService.PasswordAsync(passwordAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Authenticate, AuthorisationAction.Create));
        }

        public Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationApplicationService.RefreshTokenAsync(refreshTokenAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Authenticate, AuthorisationAction.Create));
        }

        public Task<JwtAdto> ClientCredentialAsync(ClientCredentialAdto clientCredentialAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationApplicationService.ClientCredentialAsync(clientCredentialAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Authenticate, AuthorisationAction.Create));
        }
    }
}