using System;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.Session)]
    [Link(LinkDictionary.Identity, RouteDictionary.GetIdentity, HttpVerb.Get)]
    [Link(LinkDictionary.Logout, RouteDictionary.Logout, HttpVerb.Post)]
    public class SessionResource : IEntityResource
    {
        public Guid Id { get; set; }

        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}