using System;
using Common.Api.Concurrency;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Resources;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationService)]
    [SelfLink(RouteDictionary.GetAuthenticationService, HttpVerb.Get | HttpVerb.Put)]
    public class AuthenticationServiceResource : VersionedResource
    {
        [Hidden]
        [ReadOnly]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Name { get; set; }

        [Required]
        [MaxLength(FieldSizes.Default)]
        //TODO Password attribute
        public string ClientId { get; set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        //TODO Password attribute
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