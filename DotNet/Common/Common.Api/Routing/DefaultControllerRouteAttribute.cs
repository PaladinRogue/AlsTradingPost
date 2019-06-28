using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Routing
{
    public class DefaultControllerRouteAttribute : RouteAttribute
    {
        public DefaultControllerRouteAttribute(string controllerRoute = null) : base($"api/{controllerRoute ?? "[controller]"}")
        {
        }
    }
}