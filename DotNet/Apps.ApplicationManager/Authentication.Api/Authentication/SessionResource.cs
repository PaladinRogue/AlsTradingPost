using System;
using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Authentication
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