using Common.Api.Links;
using Common.Api.PropertyTypes;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Resources;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticatePassword)]
    [SelfLink(RouteDictionary.AuthenticatePasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticatePassword)]
    public class PasswordTemplate : ITemplate
    {
        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Identifier { get; set; }

        [Required]
        [MaxLength(FieldSizes.Default)]
        [Password]
        public string Password { get; set; }
    }
}