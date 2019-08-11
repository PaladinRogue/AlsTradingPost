using System;
using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.RegisterPassword)]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticatePasswordResourceTemplate, HttpVerb.Get)]
    public class RegisterPasswordResource : IEntityResource
    {
        public Guid Id { get; set; }
    }
}