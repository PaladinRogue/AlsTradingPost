using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Status
{
    [SelfLink(RouteDictionary.Status, HttpVerb.Get)]
    public class StatusResource : IResource
    {
    }
}
