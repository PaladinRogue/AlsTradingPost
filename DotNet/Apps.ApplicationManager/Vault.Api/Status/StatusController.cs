using Common.Api.Builders.Resource;
using Vault.Setup.Infrastructure.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vault.Api.Status
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
