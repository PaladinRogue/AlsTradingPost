using Common.Api.Routing;

namespace Authentication.Setup.Infrastructure.Routing
{
    public class AuthenticationControllerRouteAttribute : ControllerRouteAttribute
    {
        public AuthenticationControllerRouteAttribute(string controllerName = null) : base("authentication", controllerName)
        {
        }
    }
}