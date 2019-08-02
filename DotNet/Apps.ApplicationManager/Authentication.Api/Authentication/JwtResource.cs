using System;
using Common.Api.Authentication;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.Jwt)]
    [Link(LinkDictionary.Identity, RouteDictionary.GetIdentity, HttpVerb.Get)]
    [Link(LinkDictionary.Logout, RouteDictionary.Logout, HttpVerb.Post)]
    public class JwtResource : IJwtResource
    {
        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }

        public Guid SessionId { get; set; }
    }
}