using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Routing
{
    public abstract class ControllerRouteAttribute : RouteAttribute
    {
        protected ControllerRouteAttribute(string applicationPrefix, string controllerName = null) : base($"{applicationPrefix}/{controllerName ?? "[controller]"}")
        {
        }
    }
}