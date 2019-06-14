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

            IEnumerable<Dictionary<string, string>> dictionaries = new List<Dictionary<string, string>>
            {
                typeof(TRouteData).GetProperties()
                    .Where(p => p.GetValue(routeData) != null)
                    .ToDictionary(
                        p => p.Name.ToCamelCase(),
                        p => p.GetValue(routeData).ToString()
                    ),
                _httpContextAccessor.HttpContext.GetRouteData().Values.ToDictionary(r => r.Key, r=> r.Value.ToString())
            };

            Dictionary<string,string> dictionary = dictionaries.SelectMany(dict => dict)
                .ToLookup(pair => pair.Key, pair => pair.Value)
                .ToDictionary(group => group.Key, group => group.First());

            return FormatRoute(RouteToCamelCase(template).Format(dictionary));
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