using System;
using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication;
using ApplicationManager.ApplicationServices.Authentication.Models;
using ApplicationManager.Setup.Infrastructure.Routing;
using Common.Api.Builders.Resource;
using Common.ApplicationServices.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationManager.Api.Authentication
{
    [ApplicationManagerControllerRoute("authenticate")]
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
            JwtAdto jwt = await _authenticationApplicationService.PasswordAsync(new PasswordAdto
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
            JwtAdto jwt = await _authenticationApplicationService.RefreshTokenAsync(new RefreshTokenAdto
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
        [HttpGet("clientCredential/{id}/resourceTemplate", Name = RouteDictionary.AuthenticateClientCredentialResourceTemplate)]
        public IActionResult GetClientCredentialResourceTemplate(Guid id)
        {
            return Ok(_resourceBuilder.Build(new ClientCredentialTemplate
            {
                Id = id
            }));
        }

        [AllowAnonymous]
        [HttpPost("clientCredential/{id}", Name = RouteDictionary.AuthenticateClientCredential)]
        public async Task<IActionResult> ClientCredential(Guid id, ClientCredentialTemplate template)
        {
            JwtAdto jwt = await _authenticationApplicationService.ClientCredentialAsync(new ClientCredentialAdto
            {
                Id = id,
                Token = template.Token,
                RedirectUri = template.RedirectUri
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