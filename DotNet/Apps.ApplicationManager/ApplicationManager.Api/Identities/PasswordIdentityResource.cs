using Common.Api.Concurrency;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Identities
{
    [ResourceType(ResourceTypes.PasswordIdentity)]
    [SelfLink(RouteDictionary.GetPasswordIdentity, HttpVerb.Get)]
    public class PasswordIdentityResource : VersionedResource
    {
        public string Identifier { get; set; }

        public string Password { get; set; }
    }
}