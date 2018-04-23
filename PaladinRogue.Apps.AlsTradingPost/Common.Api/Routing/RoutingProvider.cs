using System.Collections.Generic;
using System.Linq;

namespace Common.Api.Routing
{
    public static class RoutingProvider
    {
        private static IList<Route> _routes;

        public static void RegisterRoutes(IEnumerable<Route> routes)
        {
            _routes = routes.ToList();
        }
        
        public static string GetRouteTemplate(string routeName)
        {
            return _routes.FirstOrDefault(x => x.Name == routeName)?.Template;
        }
    }

    public class Route
    {
        public string Name { get; set; }
        public string Template { get; set; }
    }
}