using Common.Api.Authentication;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Resources
{
    [SelfLink(RouteDictionary.AuthenticationFacebook, HttpVerbs.Get)]
    public class FacebookJwtResource : JwtResource
    {
        [ReadOnly]
        [Hidden]
        public string AccessToken { get; set; }
    }
}
