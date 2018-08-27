using System.Collections.Generic;
using AlsTradingPost.Resources;
using AlsTradingPost.Resources.Authorization;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Routing;
using Common.Api.Settings;
using Common.Setup.Infrastructure.Constants;
using Microsoft.Extensions.Options;

namespace AlsTradingPost.Setup.Infrastructure.Links
{
    public class AuthenticationDynmicLinksProvider : IDynamicLinksProvider
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IRouteProvider<PersonaFlags> _routeProvider;
        private readonly AppSettings _appSettings;

        public AuthenticationDynmicLinksProvider(
            IOptions<AppSettings> appSettingsAccessor,
            ICurrentUserProvider currentUserProvider,
            IRouteProvider<PersonaFlags> routeProvider
        )
        {
            _currentUserProvider = currentUserProvider;
            _routeProvider = routeProvider;
            _appSettings = appSettingsAccessor.Value;
        }

        public IEnumerable<ILink> GetLinks()
        {
            if (_currentUserProvider.IsAuthenticated)
            {
                return new List<ILink>();
            }

            return new List<ILink>
            {
                new Link
                {
                    Name = LinkDictionary.AuthenticationServices,
                    Uri = _appSettings.AuthenticationUrl + _routeProvider.GetRouteTemplate<object>(RouteDictionary.AuthenticationServices, PersonaFlags.None, null),
                    AllowVerbs = HttpVerb.Get
                }
            };

        }
    }
}