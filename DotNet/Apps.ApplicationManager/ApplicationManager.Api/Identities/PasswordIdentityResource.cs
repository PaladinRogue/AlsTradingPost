using System;
using ApplicationManager.ApplicationServices;
using Common.Api.Concurrency;
using Common.Api.Links;
using Common.Api.PropertyTypes;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Identities
{
    [ResourceType(ResourceTypes.Password)]
    [SelfLink(RouteDictionary.GetPasswordIdentity, HttpVerb.Get)]
    [Link(LinkDictionary.ChangePassword, RouteDictionary.ChangePasswordResourceTemplate, HttpVerb.Get)]
    public class PasswordIdentityResource : VersionedResource
    {
        [Ignore]
        public Guid IdentityId { get; set; }

        public string Identifier { get; set; }

        [Password]
        public string Password { get; set; }
    }
}