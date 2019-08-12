using PaladinRogue.Library.Core.Setup.Infrastructure.Routing;

namespace PaladinRogue.Vault.Setup.Infrastructure.Routing
{
    public class VaultControllerRouteAttribute : ControllerRouteAttribute
    {
        public VaultControllerRouteAttribute(string controllerName = null) : base("vault", controllerName)
        {
        }
    }
}