using Common.Api.Authentication.Interfaces;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Resources
{
    [SelfLink(RouteDictionary.AuthenticationFacebook, HttpVerbs.Get)]
    public class FacebookJwtResource : IJwtResource
    {
        [ReadOnly]
        [Hidden]
        public string AccessToken { get; set; }
        [ReadOnly]
        [Hidden]
        public string AuthToken { get; set; }
        [ReadOnly]
        [Hidden]
        public string RefreshToken { get; set; }
        [ReadOnly]
        [Hidden]
        public int ExpiresIn { get; set; }
    }
}
