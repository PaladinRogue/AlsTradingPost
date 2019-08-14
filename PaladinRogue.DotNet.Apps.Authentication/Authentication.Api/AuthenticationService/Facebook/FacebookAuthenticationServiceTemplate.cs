using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.PropertyTypes;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Api.Validation.Attributes;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Facebook
{
    [ResourceType(ResourceTypes.AuthenticationService)]
    [SelfLink(RouteDictionary.FacebookAuthenticationServiceResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.CreateFacebookAuthenticationService)]
    public class FacebookAuthenticationServiceTemplate : ITemplate
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

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string AppAccessToken { get; set; }
    }
}