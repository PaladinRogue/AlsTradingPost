using AlsTradingPost.Setup.Infrastructure.Links;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationLoginTemplate, HttpVerbs.Get)]
    [Link(LinkDictionary.AuthenticationLogin, RouteDictionary.AuthenticationLogin, HttpVerbs.Post)]
    public class AuthenticationTemplate : ITemplate
    {
    }
}
