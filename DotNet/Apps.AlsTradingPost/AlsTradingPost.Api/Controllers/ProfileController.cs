using AlsTradingPost.Api.Profile;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Builders.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IResourceBuilder _resourceBuilder;

        public ProfileController(IResourceBuilder resourceBuilder)
        {
            _resourceBuilder = resourceBuilder;
        }

        [AllowAnonymous]
        [HttpGet("", Name = RouteDictionary.Profile)]
        public IActionResult Get()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new ProfileResource())
            );
        }
    }
}
