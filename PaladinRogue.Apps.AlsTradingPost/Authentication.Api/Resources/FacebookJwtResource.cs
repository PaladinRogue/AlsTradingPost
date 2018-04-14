using Common.Api.Authentication.Interfaces;

namespace Authentication.Api.Resources
{
    public class FacebookJwtResource : IJwtResource
    {
        public string AccessToken { get; set; }
        public string AuthToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
