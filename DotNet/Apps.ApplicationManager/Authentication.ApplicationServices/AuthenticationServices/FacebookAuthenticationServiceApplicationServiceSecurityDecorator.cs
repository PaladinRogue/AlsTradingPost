using System.Threading.Tasks;
using Authentication.ApplicationServices.AuthenticationServices.Models.Facebook;
using Authorisation.Application;
using Authorisation.Application.ApplicationServices;
using Authorisation.Application.Contexts;

namespace Authentication.ApplicationServices.AuthenticationServices
{
    public class FacebookAuthenticationServiceApplicationServiceSecurityDecorator : IFacebookAuthenticationServiceApplicationService
    {
        private readonly ISecurityApplicationService _securityApplicationService;

        private readonly IFacebookAuthenticationServiceApplicationService _facebookAuthenticationServiceApplicationService;

        public FacebookAuthenticationServiceApplicationServiceSecurityDecorator(
            ISecurityApplicationService securityApplicationService,
            IFacebookAuthenticationServiceApplicationService facebookAuthenticationServiceApplicationService)
        {
            _securityApplicationService = securityApplicationService;
            _facebookAuthenticationServiceApplicationService = facebookAuthenticationServiceApplicationService;
        }

        public Task<FacebookAdto> CreateAsync(CreateFacebookAdto createFacebookAdto)
        {
            return _securityApplicationService.SecureAsync(() => _facebookAuthenticationServiceApplicationService.CreateAsync(createFacebookAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Create));
        }

        public Task<FacebookAdto> GetAsync(GetFacebookAdto getFacebookAdto)
        {
            return _securityApplicationService.SecureAsync(() => _facebookAuthenticationServiceApplicationService.GetAsync(getFacebookAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Get));
        }

        public Task<FacebookAdto> ChangeAsync(ChangeFacebookAdto changeFacebookAdto)
        {
            return _securityApplicationService.SecureAsync(() => _facebookAuthenticationServiceApplicationService.ChangeAsync(changeFacebookAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Update));
        }
    }
}