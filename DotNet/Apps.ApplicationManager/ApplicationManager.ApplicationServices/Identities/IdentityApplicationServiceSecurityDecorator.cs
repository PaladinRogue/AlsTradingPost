using System;
using ApplicationManager.ApplicationServices.Identities.Models;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.Domain.Persistence;

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

        public PasswordIdentityAdto CreateConfirmedPasswordIdentity(CreateConfirmedPasswordIdentityAdto createConfirmedPasswordIdentityAdto)
        {
            return _securityApplicationService.Secure(() => _identityApplicationService.CreateConfirmedPasswordIdentity(createConfirmedPasswordIdentityAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Identity, AuthorisationAction.Update));
        }
    }
}