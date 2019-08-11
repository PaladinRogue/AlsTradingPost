using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaladinRogue.Libray.Core.Api.Builders.Resource;
using PaladinRogue.Vault.Setup.Infrastructure.Routing;

namespace PaladinRogue.Vault.Api.Status
{
    [VaultControllerRoute]
    public class StatusController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        public StatusController(IResourceBuilder resourceBuilder)
        {
            _resourceBuilder = resourceBuilder;
        }

        [AllowAnonymous]
        [HttpGet("", Name = RouteDictionary.Status)]
        public IActionResult Get()
        {
            return Ok(
                _resourceBuilder.Build(new StatusResource())
            );
        }
    }
}
