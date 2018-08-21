using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationFacebookTemplate, HttpVerb.Get)]
    [Link(LinkDictionary.AuthenticationFacebook, RouteDictionary.AuthenticationFacebook, HttpVerb.Post)]
    public class AuthenticationTemplate : ITemplate
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
