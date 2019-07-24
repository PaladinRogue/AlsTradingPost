using Common.Api.Links;
using Common.Api.PropertyTypes;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.ForgotPassword)]
    [SelfLink(RouteDictionary.ForgotPasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.ForgotPassword)]
    public class ForgotPasswordTemplate : ITemplate
    {
        [Required]
        [EmailAddress]
        [MaxLength(FieldSizes.Extended)]
        public string EmailAddress { get; set; }
    }
}