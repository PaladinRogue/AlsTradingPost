using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Resources;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticatePassword)]
    [SelfLink(RouteDictionary.AuthenticatePasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticatePassword)]
    public class PasswordResourceTemplate : ITemplate
    {
        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Identifier { get; set; }

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Password { get; set; }
    }
}