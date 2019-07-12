using Common.Api.Routing;

namespace ApplicationManager.Setup.Infrastructure.Routing
{
    public class ApplicationManagerControllerRouteAttribute : ControllerRouteAttribute
    {
        public ApplicationManagerControllerRouteAttribute(string controllerName = null) : base("apps", controllerName)
        {
        }
    }
}