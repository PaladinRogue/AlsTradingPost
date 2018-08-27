using Common.Api.Meta;
using Common.Api.Validation.Attributes;

namespace Authentication.Api.Authentication
{
    public class FacebookJwtResource : JwtResource
    {
        [ReadOnly]
        [Hidden]
        [Required]
        public string AccessToken { get; set; }
    }
}
