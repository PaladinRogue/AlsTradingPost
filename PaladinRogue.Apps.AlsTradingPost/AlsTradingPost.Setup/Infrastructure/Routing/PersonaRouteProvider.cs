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
    public class PersonaRouteProvider : IRouteProvider<PersonaFlags>
    {
        private readonly IDictionary<string, Route<PersonaFlags>> _routes;

        public PersonaRouteProvider(IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
        {
            _routes = new Dictionary<string, Route<PersonaFlags>>();
            
            foreach (ApiDescriptionGroup apiDescriptionGroup in apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
            {
                foreach (ApiDescription apiDescription in apiDescriptionGroup.Items)
                {
                    string routeName = apiDescription.ActionDescriptor.AttributeRouteInfo.Name;
                    if (string.IsNullOrEmpty(routeName)) continue;
                    
                    _routes.Add(
                        routeName,
                        Route<PersonaFlags>.Create(apiDescription.RelativePath, GetPersonaPolicy(apiDescription))
                    );
                }
            }
        }

        public bool HasAccessToRoute(string routeName, PersonaFlags currentPersonaFlags)
        {
            return _routes.ContainsKey(routeName) && currentPersonaFlags.HasFlag(_routes[routeName].Restriction);
        }
        
        public string GetRouteTemplate<T>(string routeName, PersonaFlags currentPersonaFlags, T routeData)
        {
            string template = GetRouteTemplate(routeName, currentPersonaFlags);

            if (string.IsNullOrEmpty(template))
            {
                return null;
            }

            Dictionary<string, string> dictionary = routeData.GetType().GetProperties()
                .Where(p => p.GetValue(routeData) != null)
                .ToDictionary(
                    p => p.Name.ToCamelCase(),
                    p => p.GetValue(routeData).ToString()
                );

            return FormatRoute(RouteToCamelCase(template).Format(dictionary));
        }

        private static PersonaFlags GetPersonaPolicy(ApiDescription apiDescription)
        {
            List<string> policies = new List<string>();

            if (!(apiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
                return PersonaFlags.None;
            
            policies.AddRange(controllerActionDescriptor.MethodInfo.GetCustomAttributes<AuthorizeAttribute>().Select(a => a.Policy));
            policies.AddRange(controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthorizeAttribute>().Select(a => a.Policy));

            PersonaFlags persona = PersonaFlags.None;
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
        
        private string GetRouteTemplate(string routeName, PersonaFlags currentPersonaFlags)
        {
            return HasAccessToRoute(routeName, currentPersonaFlags) ?  _routes[routeName].Template : null;
        }
    }
}