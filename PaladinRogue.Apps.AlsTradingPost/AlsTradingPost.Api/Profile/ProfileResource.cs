using AlsTradingPost.Setup.Infrastructure.Links;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Profile
{
    [SelfLink(RouteDictionary.Profile, HttpVerb.Get)]
    [AbsoluteLink(typeof(AuthenticationAbsoluteLinkProvider), LinkDictionary.AuthenticationServices, RouteDictionary.AuthenticationServices, HttpVerb.Get)]
    public class ProfileResource : IResource
    {
    }
}
