using System;
using Common.Api.Authentication.Interfaces;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Authentication
{
    [Link(LinkDictionary.AuthenticationRefreshToken, RouteDictionary.AuthenticationRefreshToken, HttpVerbs.Post)]
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
