using Microsoft.Extensions.Options;
using PaladinRogue.Libray.Core.Common.Settings;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Routing;

namespace PaladinRogue.Libray.Core.Api.Routing
{
    public class DefaultAbsoluteRouteProvider : IAbsoluteRouteProvider
    {
        private readonly IRouteProvider<bool> _routeProvider;

        private readonly HostSettings _hostSettings;

        public DefaultAbsoluteRouteProvider(IRouteProvider<bool> routeProvider,
            IOptions<HostSettings> hostSettingsAccessor)
        {
            _routeProvider = routeProvider;
            _hostSettings = hostSettingsAccessor.Value;
        }

        public string GetRouteTemplate<TRouteData>(string routeName,
            TRouteData routeData)
        {
            if (!_routeProvider.HasAccessToRoute(routeName, true)) return string.Empty;

            string routeTemplate = _routeProvider.GetRouteTemplate(routeName, true, routeData);

            return $"{_hostSettings.Urls}{routeTemplate}";
        }
    }
}