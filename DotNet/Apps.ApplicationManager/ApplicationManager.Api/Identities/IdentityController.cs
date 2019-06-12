using System;
using Common.Api.Builders.Resource;
using Common.Api.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationManager.Api.Identities
{
    [DefaultControllerRoute("identities")]
    public class IdentityController : ControllerBase 
    {
        private readonly IResourceBuilder _resourceBuilder;

        public IdentityController(IResourceBuilder resourceBuilder)
        {
            _resourceBuilder = resourceBuilder;
        }

        [AllowAnonymous]
        [HttpGet("{identityId}/password/resourceTemplate", Name = RouteDictionary.IdentityPasswordResourceTemplate)]
        public IActionResult Get(Guid identityId, [FromQuery]string token)
        {
            return new ObjectResult(
                _resourceBuilder.Build(new CreatePasswordIdentityResource(token))
            );
        }
    }
}
