using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationLoginTemplate, HttpVerbs.Get)]
    [Link(LinkDictionary.AuthenticationFacebook, RouteDictionary.AuthenticationLogin, HttpVerbs.Post)]
    public class AuthenticationTemplate : ITemplate
    {
    }
}
