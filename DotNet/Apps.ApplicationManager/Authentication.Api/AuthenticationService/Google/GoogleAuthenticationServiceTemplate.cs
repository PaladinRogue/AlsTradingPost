using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.PropertyTypes;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Api.Validation.Attributes;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Google
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