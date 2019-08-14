using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Vault.Api.Status
{
    [ResourceType(ResourceTypes.Status)]
    [SelfLink(RouteDictionary.Status, HttpVerb.Get)]
    public class StatusResource : IResource
    {
    }
}
