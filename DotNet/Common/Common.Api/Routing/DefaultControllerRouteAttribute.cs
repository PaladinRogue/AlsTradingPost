using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Routing
{
    public class DefaultControllerRouteAttribute : RouteAttribute
    {
        public DefaultControllerRouteAttribute(string controllerRoute) : base($"api/{controllerRoute}")
        {
        }
    }
}