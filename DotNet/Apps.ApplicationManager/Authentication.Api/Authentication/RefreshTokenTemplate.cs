using System;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateRefreshToken)]
    [SelfLink(RouteDictionary.AuthenticateRefreshTokenResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateRefreshToken)]
    public class RefreshTokenTemplate : ITemplate
    {
        [Required]
        public Guid SessionId { get; set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string Token { get; set; }
    }
}