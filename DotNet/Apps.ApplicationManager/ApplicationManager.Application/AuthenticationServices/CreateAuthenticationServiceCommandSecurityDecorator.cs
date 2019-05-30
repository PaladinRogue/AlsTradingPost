using System;
using ApplicationManager.ApplicationServices.AuthenticationServices.Interfaces;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;

namespace ApplicationManager.ApplicationServices.AuthenticationServices
{
    public class CreateAuthenticationServiceCommandSecurityDecorator : ISecure<ICreateAuthenticationServiceCommand>, ICreateAuthenticationServiceCommand
    {
        private readonly ICreateAuthenticationServiceCommand _createAuthenticationServiceCommand;
        private readonly ISecurityApplicationService _securityApplicationService;

        public CreateAuthenticationServiceCommandSecurityDecorator(
            ICreateAuthenticationServiceCommand createAuthenticationServiceCommand,
            ISecurityApplicationService securityApplicationService)
        {
            _createAuthenticationServiceCommand = createAuthenticationServiceCommand;
            _securityApplicationService = securityApplicationService;
        }

        public ICreateAuthenticationServiceCommand Service => this;

        public Guid ClientCredential(
            CreateAuthenticationGrantTypeClientCredentialAdto createAuthenticationGrantTypeClientCredentialAdto)
        {
            return _securityApplicationService.Secure(
                () => _createAuthenticationServiceCommand.ClientCredential(createAuthenticationGrantTypeClientCredentialAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.AuthenticationService, AuthorisationAction.Create));
        }
    }
}
