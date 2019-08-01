using Common.Api.Routing;

namespace Notifications.Setup.Infrastructure.Routing
{
    public class NotificationsControllerRouteAttribute : ControllerRouteAttribute
    {
        public NotificationsControllerRouteAttribute(string controllerName = null) : base("notifications", controllerName)
        {
        }
    }
}