using System;
using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Api.Validation.Attributes;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

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