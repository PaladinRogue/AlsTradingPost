using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication;
using ApplicationManager.ApplicationServices.Authentication.Models;
using Common.Api.Builders.Resource;
using Common.Api.Routing;
using Common.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationManager.Api.Authentication
{
    [DefaultControllerRoute("authenticate")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly IAuthenticationApplicationService _authenticationApplicationService;

        public AuthenticationController(
            IResourceBuilder resourceBuilder,
            IAuthenticationApplicationService authenticationApplicationService)
        {
            _resourceBuilder = resourceBuilder;
            _authenticationApplicationService = authenticationApplicationService;
        }

        [AllowAnonymous]
        [HttpGet("password/resourceTemplate", Name = RouteDictionary.AuthenticatePasswordResourceTemplate)]
        public IActionResult GetPasswordResourceTemplate()
        {
            return Ok(_resourceBuilder.Build(new PasswordResourceTemplate()));
        }

        [AllowAnonymous]
        [HttpPost("password", Name = RouteDictionary.AuthenticatePassword)]
        public async Task<IActionResult> Password(PasswordResourceTemplate passwordResourceTemplate)
        {
            JwtAdto jwt = await _authenticationApplicationService.Password(new PasswordAdto
            {
                Identifier = passwordResourceTemplate.Identifier,
                Password = passwordResourceTemplate.Password
            });

            return Ok(_resourceBuilder.Build(new JwtResource
            {
                AuthToken = jwt.AuthToken,
                ExpiresIn = jwt.ExpiresIn,
                SessionId = jwt.SessionId
            }));
        }
    }
}