using System;
using Common.Api.Authentication.Interfaces;
using Common.Api.Meta;

namespace Common.Api.Authentication
{
    public class JwtResource : IJwtResource
    {
	    [ReadOnly]
	    [Hidden]
	    public string AuthToken { get; set; }
	    [ReadOnly]
	    [Hidden]
	    public string RefreshToken { get; set; }
	    [ReadOnly]
	    [Hidden]
	    public int ExpiresIn { get; set; }
	    [ReadOnly]
	    [Hidden]
	    public Guid SessionId { get; set; }
    }
}
