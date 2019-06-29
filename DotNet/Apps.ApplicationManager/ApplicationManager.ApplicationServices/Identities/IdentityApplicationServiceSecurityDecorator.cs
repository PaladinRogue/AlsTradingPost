using ApplicationManager.ApplicationServices.Identities.Authorisation;
using ApplicationManager.ApplicationServices.Identities.Models;
using Common.Authorisation;
using Common.Authorisation.ApplicationServices;
using Common.Authorisation.Contexts;

namespace ApplicationManager.ApplicationServices.Identities
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

        public IdentityAdto Get(GetIdentityAdto getIdentityAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.Get(getIdentityAdto),
                IdentityAuthorisationContext.Create(getIdentityAdto.Id, AuthorisationAction.Get));
        }

        public void ResetPassword(ResetPasswordAdto resetPasswordAdto)
        {
            _securityApplicationService.Secure(() => { _identityApplicationService.ResetPassword(resetPasswordAdto); },
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Create));
        }

        public void ForgotPassword(ForgotPasswordAdto forgotPasswordAdto)
        {
            _securityApplicationService.Secure(() => { _identityApplicationService.ForgotPassword(forgotPasswordAdto); },
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Create));
        }

        public void ConfirmIdentity(ConfirmIdentityAdto confirmIdentityAdto)
        {
            _securityApplicationService.Secure(() => { _identityApplicationService.ConfirmIdentity(confirmIdentityAdto); },
                IdentityAuthorisationContext.Create(confirmIdentityAdto.IdentityId, AuthorisationAction.Update));
        }

        public PasswordIdentityAdto GetPasswordIdentity(GetPasswordIdentityAdto getPasswordIdentityAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.GetPasswordIdentity(getPasswordIdentityAdto),
                IdentityAuthorisationContext.Create(getPasswordIdentityAdto.IdentityId, AuthorisationAction.Get));
        }

        public PasswordIdentityAdto ChangePassword(ChangePasswordAdto changePasswordAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.ChangePassword(changePasswordAdto),
                IdentityAuthorisationContext.Create(changePasswordAdto.IdentityId, AuthorisationAction.Update));
        }

        public PasswordIdentityAdto RegisterPassword(RegisterPasswordAdto registerPasswordAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.RegisterPassword(registerPasswordAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Create));
        }

        public RefreshTokenIdentityAdto CreateRefreshToken(CreateRefreshTokenAdto createRefreshTokenAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.CreateRefreshToken(createRefreshTokenAdto),
                IdentityAuthorisationContext.Create(createRefreshTokenAdto.IdentityId, AuthorisationAction.Create));
        }

        public void Logout(LogoutAdto logoutAdto)
        {
            _securityApplicationService.Secure(() => { _identityApplicationService.Logout(logoutAdto); },
                IdentityAuthorisationContext.Create(logoutAdto.IdentityId, AuthorisationAction.Update));
        }
    }
}