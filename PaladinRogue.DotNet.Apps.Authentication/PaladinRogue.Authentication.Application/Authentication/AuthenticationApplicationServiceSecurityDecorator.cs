using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.Authentication.Models;
using PaladinRogue.Library.Authorisation.Common;
using PaladinRogue.Library.Authorisation.Common.Authorisation;
using PaladinRogue.Library.Authorisation.Common.Contexts;
using PaladinRogue.Library.Core.Application.Authentication;

namespace PaladinRogue.Authentication.Application.Authentication
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

        public Task<JwtAdto> GoogleAsync(ClientCredentialAdto clientCredentialAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationApplicationService.GoogleAsync(clientCredentialAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Authenticate, AuthorisationAction.Create));
        }

        public Task<JwtAdto> FacebookAsync(ClientCredentialAdto clientCredentialAdto)
        {
            return _securityApplicationService.SecureAsync(() => _authenticationApplicationService.FacebookAsync(clientCredentialAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Authenticate, AuthorisationAction.Create));
        }
    }
}