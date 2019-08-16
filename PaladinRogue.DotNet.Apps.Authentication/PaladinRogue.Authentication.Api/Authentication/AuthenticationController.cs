using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaladinRogue.Authentication.Application.Authentication;
using PaladinRogue.Authentication.Application.Authentication.Models;
using PaladinRogue.Authentication.Setup.Infrastructure.Routing;
using PaladinRogue.Library.Core.Api.Builders.Resource;
using PaladinRogue.Library.Core.Application.Authentication;

namespace PaladinRogue.Authentication.Api.Authentication
{
    [AuthenticationControllerRoute("authenticate")]
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

            return Ok(_resourceBuilder.Build(new SessionResource
            {
                Id = jwt.SessionId,
                AuthToken = jwt.AuthToken,
                ExpiresIn = jwt.ExpiresIn
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

            return Ok(_resourceBuilder.Build(new SessionResource
            {
                Id = jwt.SessionId,
                AuthToken = jwt.AuthToken,
                ExpiresIn = jwt.ExpiresIn
            }));
        }

        [AllowAnonymous]
        [HttpGet("google/{id}/resourceTemplate", Name = RouteDictionary.AuthenticateGoogleResourceTemplate)]
        public IActionResult GetGoogleResourceTemplate(Guid id)
        {
            return Ok(_resourceBuilder.Build(new AuthenticateGoogleTemplate
            {
                Id = id
            }));
        }

        [AllowAnonymous]
        [HttpPost("google/{id}", Name = RouteDictionary.AuthenticateGoogle)]
        public async Task<IActionResult> Google(Guid id, AuthenticateGoogleTemplate template)
        {
            JwtAdto jwt = await _authenticationApplicationService.GoogleAsync(new ClientCredentialAdto
            {
                Id = id,
                Token = template.Token,
                RedirectUri = template.RedirectUri
            });

            return Ok(_resourceBuilder.Build(new SessionResource
            {
                Id = jwt.SessionId,
                AuthToken = jwt.AuthToken,
                ExpiresIn = jwt.ExpiresIn
            }));
        }

        [AllowAnonymous]
        [HttpGet("facebook/{id}/resourceTemplate", Name = RouteDictionary.AuthenticateFacebookResourceTemplate)]
        public IActionResult GetClientCredentialResourceTemplate(Guid id)
        {
            return Ok(_resourceBuilder.Build(new AuthenticateFacebookTemplate
            {
                Id = id
            }));
        }

        [AllowAnonymous]
        [HttpPost("facebook/{id}", Name = RouteDictionary.AuthenticateFacebook)]
        public async Task<IActionResult> ClientCredential(Guid id, AuthenticateFacebookTemplate template)
        {
            JwtAdto jwt = await _authenticationApplicationService.FacebookAsync(new ClientCredentialAdto
            {
                Id = id,
                Token = template.Token,
                RedirectUri = template.RedirectUri
            });

            return Ok(_resourceBuilder.Build(new SessionResource
            {
                Id = jwt.SessionId,
                AuthToken = jwt.AuthToken,
                ExpiresIn = jwt.ExpiresIn
            }));
        }
    }
}