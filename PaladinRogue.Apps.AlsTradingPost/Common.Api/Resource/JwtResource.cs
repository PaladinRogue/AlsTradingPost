using Common.Api.Resource.Interfaces;

namespace Common.Api.Resource
{
    public class JwtResource : IJwtResource
    {
	    public string AuthToken { get; set; }
	    public int ExpiresIn { get; set; }
    }
}
