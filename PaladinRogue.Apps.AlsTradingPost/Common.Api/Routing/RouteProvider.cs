using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Resources.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore.Internal;

namespace Common.Api.Routing
{
    public class RouteProvider : IRouteProvider
    {
        private readonly IDictionary<string, Route> _routes;

        public RouteProvider(IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
        {
            _routes = new Dictionary<string, Route>();
            
            foreach (ApiDescriptionGroup apiDescriptionGroup in apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
            {
                foreach (ApiDescription apiDescription in apiDescriptionGroup.Items)
                {
                    _routes.Add(
                        apiDescription.ActionDescriptor.AttributeRouteInfo.Name,
                        Route.Create(apiDescription.RelativePath, GetControllerPolicies(apiDescription), GetActionPolicies(apiDescription))
                    );
                }
            }
        }

        public bool HasRoute(string routeName)
        {
            return _routes.ContainsKey(routeName);
        }
        
        public string GetRouteTemplate<T>(string routeName, T routeData)
        {
            string template = GetRouteTemplate(routeName);

            if (string.IsNullOrEmpty(template))
            {
                return null;
            }

            Dictionary<string, string> dictionary = typeof(T).GetProperties()
                .Where(p => p.GetValue(routeData) != null)
                .ToDictionary(
                    p => p.Name.ToCamelCase(),
                    p => p.GetValue(routeData).ToString()
                );

            return FormatRoute(RouteToCamelCase(template).Format(dictionary));
        }

        private static IEnumerable<string> GetActionPolicies(ApiDescription apiDescription)
        {
            List<string> policies = new List<string>();
            
            if (apiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                policies.AddRange(controllerActionDescriptor.MethodInfo.GetCustomAttributes<AuthorizeAttribute>().Select(a => a.Policy));
            }
            
            return policies;
        }
        
        private static IEnumerable<string> GetControllerPolicies(ApiDescription apiDescription)
        {
            List<string> policies = new List<string>();
            
            if (apiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                policies.AddRange(controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthorizeAttribute>().Select(a => a.Policy));
            }
            
            return policies;
        }

        private static string FormatRoute(string route)
        {
            return $"/{ route }";
        }

        private static string RouteToCamelCase(string route)
        {
            string[] routeParts = route.Split('/');

            IEnumerable<string> formattedRouteParts = routeParts.Select(s => s.ToCamelCase());
            
            return formattedRouteParts.Join("/");
        }
        
        private string GetRouteTemplate(string routeName)
        {
            return HasRoute(routeName) ?  _routes[routeName].Template : null;
        }
    }
}