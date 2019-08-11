using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaladinRogue.Authentication.Setup.Infrastructure.Routing;
using PaladinRogue.Libray.Core.Api.Builders.Resource;

namespace PaladinRogue.Authentication.Api.Status
{
    [AuthenticationControllerRoute]
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
