using System;
using Authentication.Setup.Infrastructure.Routing;
using Common.Api.Builders.Resource;
using Common.Setup.Infrastructure.Authorisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Profile
{
    [AuthenticationControllerRoute]
    public class ProfileController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        public ProfileController(
            IResourceBuilder resourceBuilder,
            ICurrentIdentityProvider currentIdentityProvider)
        {
            _resourceBuilder = resourceBuilder;
            _currentIdentityProvider = currentIdentityProvider;
        }

        [AllowAnonymous]
        [HttpGet("", Name = RouteDictionary.Profile)]
        public IActionResult Get()
        {
            return Ok(_resourceBuilder.Build(new IdentityResource
            {
                Id = _currentIdentityProvider.IsAuthenticated ? _currentIdentityProvider.Id : Guid.Empty
            }));
        }
    }
}