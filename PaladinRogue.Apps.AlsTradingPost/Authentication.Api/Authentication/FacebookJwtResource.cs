using Authentication.Setup.Infrastructure.Links;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Validation.Attributes;

namespace Authentication.Api.Authentication
{
    [DynamicLinks(typeof(FacebookApplicationLinksProvider))]
    public class FacebookJwtResource : JwtResource
    {
        [ReadOnly]
        [Hidden]
        [Required]
        public string AccessToken { get; set; }
    }
}
