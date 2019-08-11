using System;
using PaladinRogue.Libray.Core.Api.Concurrency;
using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Meta;
using PaladinRogue.Libray.Core.Api.PropertyTypes;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Api.Validation.Attributes;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Google
{
    [ResourceType(ResourceTypes.AuthenticationService)]
    [SelfLink(RouteDictionary.GetGoogleAuthenticationService, HttpVerb.Get | HttpVerb.Put | HttpVerb.Delete)]
    public class GoogleAuthenticationServiceResource : VersionedResource, IEntityResource
    {
        [Hidden]
        [ReadOnly]
        public Guid Id { get; set; }

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