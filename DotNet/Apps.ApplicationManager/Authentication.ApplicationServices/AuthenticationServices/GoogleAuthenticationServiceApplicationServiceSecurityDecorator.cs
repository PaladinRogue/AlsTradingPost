using System.Threading.Tasks;
using Authentication.ApplicationServices.AuthenticationServices.Models.Google;
using Common.Authorisation;
using Common.Authorisation.ApplicationServices;
using Common.Authorisation.Contexts;

namespace Authentication.ApplicationServices.AuthenticationServices
{
    public class GoogleAuthenticationServiceApplicationServiceSecurityDecorator : IGoogleAuthenticationServiceApplicationService
    {
        private readonly ISecurityApplicationService _securityApplicationService;

        private readonly IGoogleAuthenticationServiceApplicationService _googleAuthenticationServiceApplicationService;

        public GoogleAuthenticationServiceApplicationServiceSecurityDecorator(
            ISecurityApplicationService securityApplicationService,
            IGoogleAuthenticationServiceApplicationService googleAuthenticationServiceApplicationService)
        {
            _securityApplicationService = securityApplicationService;
            _googleAuthenticationServiceApplicationService = googleAuthenticationServiceApplicationService;
        }

        public Task<GoogleAdto> CreateAsync(CreateGoogleAdto createGoogleAdto)
        {
            return _securityApplicationService.SecureAsync(() => _googleAuthenticationServiceApplicationService.CreateAsync(createGoogleAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Create));
        }

        public Task<GoogleAdto> GetAsync(GetGoogleAdto getGoogleAdto)
        {
            return _securityApplicationService.SecureAsync(() => _googleAuthenticationServiceApplicationService.GetAsync(getGoogleAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Get));
        }

        public Task<GoogleAdto> ChangeAsync(ChangeGoogleAdto changeGoogleAdto)
        {
            return _securityApplicationService.SecureAsync(() => _googleAuthenticationServiceApplicationService.ChangeAsync(changeGoogleAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Update));
        }
    }
}