using Common.Api.Routing;

namespace KeyVault.Setup.Infrastructure.Routing
{
    public class KeyVaultControllerRouteAttribute : ControllerRouteAttribute
    {
        public KeyVaultControllerRouteAttribute(string controllerName = null) : base("vault", controllerName)
        {
        }
    }
}