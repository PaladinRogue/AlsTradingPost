using AlsTradingPost.Setup.Infrastructure.Links;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationLoginTemplate, HttpVerb.Get)]
    [Link(LinkDictionary.AuthenticationLogin, RouteDictionary.AuthenticationLogin, HttpVerb.Post)]
    public class AuthenticationTemplate : ITemplate
    {
    }
}
