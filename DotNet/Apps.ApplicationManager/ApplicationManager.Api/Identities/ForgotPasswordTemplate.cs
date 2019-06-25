using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Identities
{
    [ResourceType(ResourceTypes.Password)]
    [SelfLink(RouteDictionary.ForgotPasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.ForgotPassword)]
    public class ForgotPasswordTemplate : ITemplate
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}