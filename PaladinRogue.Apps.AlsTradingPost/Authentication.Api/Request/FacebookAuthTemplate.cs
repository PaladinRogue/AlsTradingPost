using Common.Api.Resources;

namespace Authentication.Api.Request
{
    public class FacebookAuthTemplate : ITemplate
    {
        public string AccessToken { get; set; }
    }
}
