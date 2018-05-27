using System.Threading.Tasks;
using Authentication.Application.Authentication.Interfaces;
using Authentication.Application.Authentication.Models;
using Common.Application.Authentication;
using Common.Application.Authorisation;

namespace Authentication.Application.Authentication
{
    public class AuthenticationSecurityApplicationService : ISecure<IAuthenticationApplicationService>,
        IAuthenticationApplicationService
    {
        private readonly ISecurityApplicationService _securityApplicationService;
        private readonly IAuthenticationApplicationService _authenticationApplicationService;

        public AuthenticationSecurityApplicationService(
            ISecurityApplicationService securityApplicationService,
            IAuthenticationApplicationService authenticationApplicationService)
        {
            _authenticationApplicationService = authenticationApplicationService;
            _securityApplicationService = securityApplicationService;
        }

        public IAuthenticationApplicationService Service => this;

        public Task<ExtendedJwtAdto> LoginAsync(LoginAdto loginAdto)
        {
            return _securityApplicationService.Secure(() => _authenticationApplicationService.LoginAsync(loginAdto),
                AuthorisationRule.Create(AuthorisationResource.Authentication, AuthenticationAuthorisationAction.Login));
        }

        public Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto)
        {
            return _securityApplicationService.Secure(
                () => _authenticationApplicationService.RefreshTokenAsync(refreshTokenAdto),
                AuthorisationRule.Create(AuthorisationResource.Authentication, AuthenticationAuthorisationAction.RefreshToken));
        }
    }
}