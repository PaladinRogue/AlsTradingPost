using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Api.Builders.Dictionary;
using Common.Resources.Extensions;
using Microsoft.EntityFrameworkCore.Internal;

namespace Common.Api.Routing
{
    public static class RoutingProvider
    {
        private static IList<Route> _routes;

        public static void RegisterRoutes(IEnumerable<Route> routes)
        {
            _routes = routes.ToList();
        }

        public static string GetRoute<T>(string routeName, T routeData)
        {
            string template = GetRouteTemplate(routeName);

            DictionaryBuilder<string, string> dictionaryBuilder = DictionaryBuilder<string, string>.Create();
            
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                dictionaryBuilder.Add(propertyInfo.Name.ToCamelCase(), propertyInfo.GetValue(routeData)?.ToString());
            }

            return $"/{ RouteToCamelCase(template).Format(dictionaryBuilder.Build()) }";
        }

        private static string RouteToCamelCase(string route)
        {
            string[] routeParts = route.Split('/');

            IEnumerable<string> formattedRouteParts = routeParts.Select(s => s.ToCamelCase());
            
            return formattedRouteParts.Join("/");
        }
        
        private static string GetRouteTemplate(string routeName)
        {
            return _routes.FirstOrDefault(x => x.Name == routeName)?.Template;
        }
    }
}