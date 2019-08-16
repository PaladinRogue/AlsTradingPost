using PaladinRogue.Library.Core.Setup.Infrastructure.Routing;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Routing
{
    public class AuthenticationControllerRouteAttribute : ControllerRouteAttribute
    {
        public AuthenticationControllerRouteAttribute(string controllerName = null) : base("authentication", controllerName)
        {
        }
    }
}