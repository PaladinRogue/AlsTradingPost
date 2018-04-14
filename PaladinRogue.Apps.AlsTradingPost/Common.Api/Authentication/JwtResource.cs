using Common.Api.Authentication.Interfaces;

namespace Common.Api.Authentication
{
    public class JwtResource : IJwtResource
    {
	    public string AuthToken { get; set; }
	    public int ExpiresIn { get; set; }
    }
}
