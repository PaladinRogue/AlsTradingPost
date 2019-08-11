using Microsoft.AspNetCore.Mvc;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Routing
{
    public abstract class ControllerRouteAttribute : RouteAttribute
    {
        protected ControllerRouteAttribute(string applicationPrefix, string controllerName = null) : base($"{applicationPrefix}/{controllerName ?? "[controller]"}")
        {
        }
    }
}