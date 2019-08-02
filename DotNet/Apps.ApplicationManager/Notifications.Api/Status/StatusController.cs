using Common.Api.Builders.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Setup.Infrastructure.Routing;

namespace Notifications.Api.Status
{
    [NotificationsControllerRoute]
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
