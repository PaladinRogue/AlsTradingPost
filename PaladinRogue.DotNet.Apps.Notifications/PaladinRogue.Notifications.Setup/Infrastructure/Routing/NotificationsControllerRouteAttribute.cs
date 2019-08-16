using PaladinRogue.Library.Core.Setup.Infrastructure.Routing;

namespace PaladinRogue.Notifications.Setup.Infrastructure.Routing
{
    public class NotificationsControllerRouteAttribute : ControllerRouteAttribute
    {
        public NotificationsControllerRouteAttribute(string controllerName = null) : base("notifications", controllerName)
        {
        }
    }
}