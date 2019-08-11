using System;
using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Api.Validation.Attributes;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateRefreshToken)]
    [SelfLink(RouteDictionary.AuthenticateRefreshTokenResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateRefreshToken)]
    public class RefreshTokenTemplate : ITemplate
    {
        [Required]
        public Guid? SessionId { get; set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string Token { get; set; }
    }
}