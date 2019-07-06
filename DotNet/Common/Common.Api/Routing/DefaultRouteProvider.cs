using System.Collections.Generic;
using System.Linq;
using Common.Resources.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;

namespace Common.Api.Routing
{
    public class DefaultRouteProvider : IRouteProvider<bool>
    {
        private readonly IDictionary<string, Route<bool>> _routes;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultRouteProvider(
            IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _routes = new Dictionary<string, Route<bool>>();

            foreach (ApiDescriptionGroup apiDescriptionGroup in apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
            {
                foreach (ApiDescription apiDescription in apiDescriptionGroup.Items)
                {
                    string routeName = apiDescription.ActionDescriptor.AttributeRouteInfo.Name;
                    if (string.IsNullOrEmpty(routeName)) continue;

                    _routes.Add(
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
            return _routes.ContainsKey(routeName);
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
            return HasAccessToRoute(routeName, true) ?  _routes[routeName].Template : null;
        }
    }
}