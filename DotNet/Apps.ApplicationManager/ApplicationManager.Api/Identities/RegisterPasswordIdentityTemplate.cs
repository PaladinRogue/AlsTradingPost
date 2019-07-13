using Common.Api.Links;
using Common.Api.PropertyTypes;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Resources;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Identities
{
    [ResourceType(ResourceTypes.Password)]
    [SelfLink(RouteDictionary.RegisterPasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.RegisterPassword)]
    public class RegisterPasswordIdentityTemplate : ITemplate
    {
        [Required]
        [EmailAddress]
        [MaxLength(FieldSizes.Extended)]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string Identifier { get; set; }

        [Required]
        [Length(6, FieldSizes.Default)]
        [Password]
        public string Password { get; set; }

        [Required]
        [Length(6, FieldSizes.Default)]
        [Password]
        public string ConfirmPassword { get; set; }
    }
}