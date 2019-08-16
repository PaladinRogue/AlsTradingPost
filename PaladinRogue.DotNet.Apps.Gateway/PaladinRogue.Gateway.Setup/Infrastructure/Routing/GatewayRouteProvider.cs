using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PaladinRogue.Library.Core.Setup.Infrastructure.Routing;

namespace PaladinRogue.Gateway.Setup.Infrastructure.Routing
{
    public class GatewayRouteProvider : DefaultRouteProvider
    {
        public GatewayRouteProvider(
            IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider,
            IHttpContextAccessor httpContextAccessor)
            : base(apiDescriptionGroupCollectionProvider, httpContextAccessor)
        {
            Routes.Add(ApplicationRouteDictionary.Entrypoint, Route<bool>.Create("{systemName}", true));
        }
    }
}