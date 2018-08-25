using System;
using AlsTradingPost.Setup.Infrastructure.Links;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Authentication.Interfaces;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Authentication
{
    [Link(LinkDictionary.AuthenticationRefreshTokenTemplate, RouteDictionary.AuthenticationRefreshTokenTemplate, HttpVerb.Get)]
    public class JwtResource : IJwtResource
    {
	    [ReadOnly]
	    [Hidden]
	    public string AuthToken { get; set; }
	    [ReadOnly]
	    [Hidden]
        [Required]
	    public string RefreshToken { get; set; }
	    [ReadOnly]
	    [Hidden]
	    public int ExpiresIn { get; set; }
	    [ReadOnly]
	    [Hidden]
        [Required]
	    public Guid SessionId { get; set; }
    }
}
