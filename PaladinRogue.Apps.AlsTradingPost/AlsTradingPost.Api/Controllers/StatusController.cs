using AlsTradingPost.Api.Status;
using Common.Api.Builders.Resource;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly IResourceBuilder _resourceBuilder;

        public StatusController(IResourceBuilder resourceBuilder)
        {
            _resourceBuilder = resourceBuilder;
        }

        [HttpGet]
        [Route("", Name = RouteDictionary.Status)]
        public IActionResult Get()
        {
            return new ObjectResult(
                _resourceBuilder.Create(new StatusResource()).Build()
            );
        }
    }
}
