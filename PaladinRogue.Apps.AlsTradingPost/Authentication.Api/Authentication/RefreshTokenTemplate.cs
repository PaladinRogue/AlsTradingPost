using System;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
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
