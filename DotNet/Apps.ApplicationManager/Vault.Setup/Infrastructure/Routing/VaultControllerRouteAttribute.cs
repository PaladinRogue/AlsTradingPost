using Common.Api.Routing;

namespace Vault.Setup.Infrastructure.Routing
{
    public class VaultControllerRouteAttribute : ControllerRouteAttribute
    {
        public VaultControllerRouteAttribute(string controllerName = null) : base("vault", controllerName)
        {
        }
    }
}