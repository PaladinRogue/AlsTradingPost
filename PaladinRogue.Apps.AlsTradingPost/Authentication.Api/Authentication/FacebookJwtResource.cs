using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationFacebook, HttpVerb.Post)]
    public class FacebookJwtResource : JwtResource
    {
        [ReadOnly]
        [Hidden]
        [Required]
        public string AccessToken { get; set; }
    }
}
