using AlsTradingPost.Application.MagicItemTemplate.Interfaces;
using AlsTradingPost.Application.MagicItemTemplate.Models;
using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;

namespace AlsTradingPost.Application.MagicItemTemplate
{
    public class MagicItemTemplateSecurityApplicationService : ISecure<IMagicItemTemplateApplicationService>,
        IMagicItemTemplateApplicationService
    {
        private readonly IMagicItemTemplateApplicationService _magicItemTemplateApplicationService;
        private readonly ISecurityApplicationService _securityApplicationService;

        public MagicItemTemplateSecurityApplicationService(
            IMagicItemTemplateApplicationService magicItemTemplateApplicationService,
            ISecurityApplicationService securityApplicationService)
        {
            _magicItemTemplateApplicationService = magicItemTemplateApplicationService;
            _securityApplicationService = securityApplicationService;
        }

        public IMagicItemTemplateApplicationService Service => this;

        public MagicItemTemplatePagedCollectionAdto Search(MagicItemTemplateSearchAdto magicItemTemplateSearchAdto)
        {
            return _securityApplicationService.Secure(
                () => _magicItemTemplateApplicationService.Search(magicItemTemplateSearchAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.MagicItemTemplate, AuthorisationAction.Search));
        }
    }
}