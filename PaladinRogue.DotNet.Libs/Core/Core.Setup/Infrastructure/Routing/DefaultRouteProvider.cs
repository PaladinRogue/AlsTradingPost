using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using PaladinRogue.Library.Core.Common.Extensions;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Routing
{
    public class DefaultRouteProvider : IRouteProvider<bool>
    {
        protected readonly IDictionary<string, Route<bool>> Routes;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultRouteProvider(
            IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Routes = new Dictionary<string, Route<bool>>();

            foreach (ApiDescriptionGroup apiDescriptionGroup in apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
            {
                foreach (ApiDescription apiDescription in apiDescriptionGroup.Items)
                {
                    string routeName = apiDescription.ActionDescriptor.AttributeRouteInfo.Name;
                    if (string.IsNullOrEmpty(routeName)) continue;

                    Routes.Add(
                        routeName,
                        Route<bool>.Create(apiDescription.RelativePath, true)
                    );
                }
            }
        }

        public string GetRouteTemplate<TRouteData>(string routeName, bool routeRestriction, TRouteData routeData)
        {
            string template = GetRouteTemplate(routeName);

            if (string.IsNullOrEmpty(template))
            {
                return null;
            }

            Dictionary<string,string> routeDataDictionary = routeData.GetType().GetProperties()
                .Where(p => p.GetValue(routeData) != null)
                .ToDictionary(
                    p => p.Name.ToCamelCase(),
                    p => p.GetValue(routeData).ToString()
                );

            RouteValueDictionary httpContextRouteData = _httpContextAccessor.HttpContext.GetRouteData().Values;

            return FormatRoute(RouteToCamelCase(template).Format(routeDataDictionary).Format(httpContextRouteData));
        }

        public bool HasAccessToRoute(string routeName, bool routeRestriction)
        {
            return Routes.ContainsKey(routeName);
        }

        private static string FormatRoute(string route)
        {
            return $"/{ route }";
        }

        private static string RouteToCamelCase(string route)
        {
            string[] routeParts = route.Split('/');

            IEnumerable<string> formattedRouteParts = routeParts.Select(s => s.ToCamelCase());

            return string.Join("/", formattedRouteParts);
        }

        private string GetRouteTemplate(string routeName)
        {
            return HasAccessToRoute(routeName, true) ?  Routes[routeName].Template : null;
        }
    }
}