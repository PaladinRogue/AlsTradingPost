using AlsTradingPost.Api.Status;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Builders.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    public class StatusController : Controller
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
            return new ObjectResult(
                _resourceBuilder.Build(new StatusResource())
            );
        }
    }
}
