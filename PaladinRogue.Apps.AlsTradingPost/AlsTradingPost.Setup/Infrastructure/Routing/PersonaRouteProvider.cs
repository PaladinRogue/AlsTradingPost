using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlsTradingPost.Resources;
using AlsTradingPost.Setup.Infrastructure.Authorization;
using Common.Api.Routing;
using Common.Resources.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore.Internal;

namespace AlsTradingPost.Setup.Infrastructure.Routing
{
    public class PersonaRouteProvider : IRouteProvider<Persona>
    {
        private readonly IDictionary<string, Route<Persona>> _routes;

        public PersonaRouteProvider(IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
        {
            _routes = new Dictionary<string, Route<Persona>>();
            
            foreach (ApiDescriptionGroup apiDescriptionGroup in apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
            {
                foreach (ApiDescription apiDescription in apiDescriptionGroup.Items)
                {
                    string routeName = apiDescription.ActionDescriptor.AttributeRouteInfo.Name;
                    if (string.IsNullOrEmpty(routeName)) continue;
                    
                    _routes.Add(
                        routeName,
                        Route<Persona>.Create(apiDescription.RelativePath, GetPersonaPolicy(apiDescription))
                    );
                }
            }
        }

        public bool HasAccessToRoute(string routeName, Persona routeRestriction)
        {
            return _routes.ContainsKey(routeName) && _routes[routeName].Restriction.HasFlag(routeRestriction);
        }
        
        public string GetRouteTemplate<T>(string routeName, Persona routeRestriction, T routeData)
        {
            string template = GetRouteTemplate(routeName, routeRestriction);

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

        private static Persona GetPersonaPolicy(ApiDescription apiDescription)
        {
            List<string> policies = new List<string>();

            if (!(apiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
                return Persona.None;
            
            policies.AddRange(controllerActionDescriptor.MethodInfo.GetCustomAttributes<AuthorizeAttribute>().Select(a => a.Policy));
            policies.AddRange(controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthorizeAttribute>().Select(a => a.Policy));

            Persona persona = Persona.None;
            foreach (string policy in policies)
            {
                persona |= PersonaPolicyMapper.FromPolicy(policy);
            }
            return persona;
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
        
        private string GetRouteTemplate(string routeName, Persona routeRestriction)
        {
            return HasAccessToRoute(routeName, routeRestriction) ?  _routes[routeName].Template : null;
        }
    }
}