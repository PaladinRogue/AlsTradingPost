using ApplicationManager.ApplicationServices.Identities.Models;
using Common.Authorisation;

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
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Get));
        }

        public PasswordIdentityAdto CreateConfirmedPasswordIdentity(CreateConfirmedPasswordIdentityAdto createConfirmedPasswordIdentityAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.CreateConfirmedPasswordIdentity(createConfirmedPasswordIdentityAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Update));
        }

        public PasswordIdentityAdto GetPasswordIdentity(GetPasswordIdentityAdto getPasswordIdentityAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.GetPasswordIdentity(getPasswordIdentityAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Get));
        }

        public PasswordIdentityAdto ChangePassword(ChangePasswordAdto changePasswordAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.ChangePassword(changePasswordAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Update));
        }

        public PasswordIdentityAdto RegisterPassword(RegisterPasswordAdto registerPasswordAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.RegisterPassword(registerPasswordAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Create));
        }
    }
}