using System;
using System.Threading.Tasks;
using ApplicationManager.ApplicationServices;
using ApplicationManager.ApplicationServices.Authentication;
using ApplicationManager.ApplicationServices.Authentication.Models;
using Common.Api.Builders.Resource;
using Common.Api.Routing;
using Common.ApplicationServices.Authentication;
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
            return Ok(_resourceBuilder.Build(new PasswordTemplate()));
        }

        [AllowAnonymous]
        [HttpPost("password", Name = RouteDictionary.AuthenticatePassword)]
        public async Task<IActionResult> Password(PasswordTemplate passwordTemplate)
        {
            JwtAdto jwt = await _authenticationApplicationService.Password(new PasswordAdto
            {
                Identifier = passwordTemplate.Identifier,
                Password = passwordTemplate.Password
            });

            return Ok(_resourceBuilder.Build(new JwtResource
            {
                AuthToken = jwt.AuthToken,
                ExpiresIn = jwt.ExpiresIn,
                SessionId = jwt.SessionId
            }));
        }

        [AllowAnonymous]
        [HttpGet("refreshToken/resourceTemplate", Name = RouteDictionary.AuthenticateRefreshTokenResourceTemplate)]
        public IActionResult GetRefreshTokenResourceTemplate()
        {
            return Ok(_resourceBuilder.Build(new RefreshTokenTemplate()));
        }

        [AllowAnonymous]
        [HttpPost("refreshToken", Name = RouteDictionary.AuthenticateRefreshToken)]
        public async Task<IActionResult> RefreshToken(RefreshTokenTemplate refreshTokenTemplate)
        {
            JwtAdto jwt = await _authenticationApplicationService.RefreshToken(new RefreshTokenAdto
            {
                SessionId = refreshTokenTemplate.SessionId,
                Token = refreshTokenTemplate.Token
            });

            return Ok(_resourceBuilder.Build(new JwtResource
            {
                AuthToken = jwt.AuthToken,
                ExpiresIn = jwt.ExpiresIn,
                SessionId = jwt.SessionId
            }));
        }

        [AllowAnonymous]
        [HttpGet("clientCredential", Name = RouteDictionary.AuthenticateClientCredential)]
        public IActionResult ClientCredential(string code, Guid state)
        {
            return Ok();
        }
    }
}