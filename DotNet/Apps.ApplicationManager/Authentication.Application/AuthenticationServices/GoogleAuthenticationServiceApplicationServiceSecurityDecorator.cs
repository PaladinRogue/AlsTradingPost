using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models.Google;
using PaladinRogue.Libray.Authorisation.Application.ApplicationServices;
using PaladinRogue.Libray.Authorisation.Common;
using PaladinRogue.Libray.Authorisation.Common.Contexts;

namespace PaladinRogue.Authentication.Application.AuthenticationServices
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