using Common.Api.Links;
using Common.Api.PropertyTypes;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService.Google
{
    [ResourceType(ResourceTypes.AuthenticationService)]
    [SelfLink(RouteDictionary.GoogleAuthenticationServiceResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.CreateGoogleAuthenticationService)]
    public class GoogleAuthenticationServiceTemplate : ITemplate
    {
        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Name { get; set; }

        [Required]
        [MaxLength(FieldSizes.Default)]
        [Password]
        public string ClientId { get; set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        [Password]
        public string ClientSecret { get; set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string ClientGrantAccessTokenUrl { get; set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string GrantAccessTokenUrl { get; set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string ValidateAccessTokenUrl { get; set; }
    }
}