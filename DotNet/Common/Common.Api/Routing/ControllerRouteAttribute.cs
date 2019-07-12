using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Routing
{
    public abstract class ControllerRouteAttribute : RouteAttribute
    {
        public ControllerRouteAttribute(string applicationPrefix, string controllerName = null) : base($"api/{applicationPrefix}/{controllerName ?? "[controller]"}")
        {
        }
    }
}