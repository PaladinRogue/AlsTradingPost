using System;
using AlsTradingPost.Setup.Infrastructure.Links;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationRefreshTokenTemplate, HttpVerb.Get)]
    [Link(LinkDictionary.AuthenticationRefreshToken, RouteDictionary.AuthenticationRefreshToken, HttpVerb.Post)]
    public class RefreshTokenTemplate : ITemplate
    {
        [Required]
        public Guid? SessionId { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
