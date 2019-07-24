using System.Threading.Tasks;
using Authentication.ApplicationServices.Identities.Authorisation;
using Authentication.ApplicationServices.Identities.Models;
using Common.Authorisation;
using Common.Authorisation.ApplicationServices;
using Common.Authorisation.Contexts;

namespace Authentication.ApplicationServices.Identities
{
    public class IdentityApplicationServiceSecurityDecorator : IIdentityApplicationService
    {
        private readonly ISecurityApplicationService _securityApplicationService;

        private readonly IIdentityApplicationService _identityApplicationService;

        public IdentityApplicationServiceSecurityDecorator(
            ISecurityApplicationService securityApplicationService,
            IIdentityApplicationService identityApplicationService)
        {
            _securityApplicationService = securityApplicationService;
            _identityApplicationService = identityApplicationService;
        }

        public Task<IdentityAdto> GetAsync(GetIdentityAdto getIdentityAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.GetAsync(getIdentityAdto),
                IdentityAuthorisationContext.Create(getIdentityAdto.Id, AuthorisationAction.Get));
        }

        public Task ResetPasswordAsync(ResetPasswordAdto resetPasswordAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.ResetPasswordAsync(resetPasswordAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Create));
        }

        public Task ForgotPasswordAsync(ForgotPasswordAdto forgotPasswordAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.ForgotPasswordAsync(forgotPasswordAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Create));
        }

        public Task ConfirmIdentityAsync(ConfirmIdentityAdto confirmIdentityAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.ConfirmIdentityAsync(confirmIdentityAdto),
                IdentityAuthorisationContext.Create(confirmIdentityAdto.IdentityId, AuthorisationAction.Update));
        }

        public Task<PasswordIdentityAdto> GetPasswordIdentityAsync(GetPasswordIdentityAdto getPasswordIdentityAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.GetPasswordIdentityAsync(getPasswordIdentityAdto),
                IdentityAuthorisationContext.Create(getPasswordIdentityAdto.IdentityId, AuthorisationAction.Get));
        }

        public Task<PasswordIdentityAdto> ChangePasswordAsync(ChangePasswordAdto changePasswordAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.ChangePasswordAsync(changePasswordAdto),
                IdentityAuthorisationContext.Create(changePasswordAdto.IdentityId, AuthorisationAction.Update));
        }

        public Task<PasswordIdentityAdto> RegisterPasswordAsync(RegisterPasswordAdto registerPasswordAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.RegisterPasswordAsync(registerPasswordAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Create));
        }

        public Task<RefreshTokenIdentityAdto> CreateRefreshTokenAsync(CreateRefreshTokenAdto createRefreshTokenAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.CreateRefreshTokenAsync(createRefreshTokenAdto),
                IdentityAuthorisationContext.Create(createRefreshTokenAdto.IdentityId, AuthorisationAction.Update));
        }

        public Task ResendConfirmIdentityAsync(ResendConfirmIdentityAdto resendConfirmIdentityAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.ResendConfirmIdentityAsync(resendConfirmIdentityAdto),
                IdentityAuthorisationContext.Create(resendConfirmIdentityAdto.IdentityId, AuthorisationAction.Update));
        }

        public Task LogoutAsync(LogoutAdto logoutAdto)
        {
            return _securityApplicationService.SecureAsync(() => _identityApplicationService.LogoutAsync(logoutAdto),
                IdentityAuthorisationContext.Create(logoutAdto.IdentityId, AuthorisationAction.Update));
        }
    }
}