using System;
using ApplicationManager.ApplicationServices.Identities;
using ApplicationManager.ApplicationServices.Identities.Models;
using Common.Api.Authentication;
using Common.Api.Builders.Resource;
using Common.Api.Routing;
using Common.Setup.Infrastructure.Authorisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationManager.Api.Identities
{
    [DefaultControllerRoute]
    public class IdentityController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly IIdentityApplicationService _identityApplicationService;

        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        public IdentityController(
            IResourceBuilder resourceBuilder,
            IIdentityApplicationService identityApplicationService,
            ICurrentIdentityProvider currentIdentityProvider)
        {
            _resourceBuilder = resourceBuilder;
            _identityApplicationService = identityApplicationService;
            _currentIdentityProvider = currentIdentityProvider;
        }

        [AllowAnonymous]
        [HttpGet("password/reset/resourceTemplate", Name = RouteDictionary.ResetPasswordResourceTemplate)]
        public IActionResult GetResetPasswordResourceTemplate([FromQuery]string token)
        {
            return Ok(_resourceBuilder.Build(new ResetPasswordTemplate
            {
                Token = token
            }));
        }

        [AllowAnonymous]
        [HttpPost("password/reset", Name = RouteDictionary.ResetPassword)]
        public IActionResult Post(ResetPasswordTemplate template)
        {
            _identityApplicationService.ResetPassword(new ResetPasswordAdto
            {
                Password = template.Password,
                ConfirmPassword = template.ConfirmPassword,
                Token = template.Token
            });

            return Accepted(_resourceBuilder.Build(new ResetPasswordResource()));
        }

        [AllowAnonymous]
        [HttpGet("password/forgot/resourceTemplate", Name = RouteDictionary.ForgotPasswordResourceTemplate)]
        public IActionResult GetForgotPasswordResourceTemplate()
        {
            return Ok(_resourceBuilder.Build(new ForgotPasswordTemplate()));
        }

        [AllowAnonymous]
        [HttpPost("password/forgot", Name = RouteDictionary.ForgotPassword)]
        public IActionResult Post(ForgotPasswordTemplate template)
        {
            _identityApplicationService.ForgotPassword(new ForgotPasswordAdto
            {
                EmailAddress = template.EmailAddress
            });

            return Accepted(_resourceBuilder.Build(new ForgotPasswordResource()));
        }

        [HttpGet("password", Name = RouteDictionary.GetPasswordIdentity)]
        public IActionResult GetPasswordIdentity()
        {
            PasswordIdentityAdto passwordIdentityAdto = _identityApplicationService.GetPasswordIdentity(new GetPasswordIdentityAdto
            {
                IdentityId = _currentIdentityProvider.Id
            });

            return Ok(_resourceBuilder.Build(new PasswordIdentityResource
            {
                Identifier = passwordIdentityAdto.Identifier,
                Password = passwordIdentityAdto.Password,
                Version = passwordIdentityAdto.Version
            }));
        }

        [HttpGet("password/change/resourceTemplate", Name = RouteDictionary.ChangePasswordResourceTemplate)]
        public IActionResult ChangePasswordResourceTemplate()
        {
            IdentityAdto identityAdto = _identityApplicationService.Get(new GetIdentityAdto
            {
                Id = _currentIdentityProvider.Id
            });

            return Ok(_resourceBuilder.Build(new ChangePasswordIdentityTemplate
            {
                Version = identityAdto.Version
            }));
        }

        [HttpPost("password/change", Name = RouteDictionary.ChangePassword)]
        public IActionResult ChangePassword(ChangePasswordIdentityTemplate template)
        {
            Guid identityId = _currentIdentityProvider.Id;

            PasswordIdentityAdto passwordIdentityAdto = _identityApplicationService.ChangePassword(new ChangePasswordAdto
            {
                IdentityId = identityId,
                Password = template.Password,
                ConfirmPassword = template.ConfirmPassword,
                Version = template.Version
            });

            return CreatedAtRoute(RouteDictionary.GetPasswordIdentity, new { identityId }, _resourceBuilder.Build(new PasswordIdentityResource
            {
                Identifier = passwordIdentityAdto.Identifier,
                Password = passwordIdentityAdto.Password,
                Version = passwordIdentityAdto.Version
            }));
        }

        [AllowRestrictedAppAccess]
        [HttpGet("confirm/resourceTemplate", Name = RouteDictionary.ConfirmIdentityResourceTemplate)]
        public IActionResult ConfirmIdentityResourceTemplate([FromQuery]string token)
        {
            return Ok(_resourceBuilder.Build(new ConfirmIdentityTemplate
            {
                Token = token
            }));
        }

        [AllowRestrictedAppAccess]
        [HttpPost("confirm", Name = RouteDictionary.ConfirmIdentity)]
        public IActionResult ConfirmIdentity(ConfirmIdentityTemplate template)
        {
            _identityApplicationService.ConfirmIdentity(new ConfirmIdentityAdto
            {
                IdentityId = _currentIdentityProvider.Id,
                Token = template.Token
            });

            return Accepted(_resourceBuilder.Build(new ConfirmIdentityResource()));
        }

        [AllowAnonymous]
        [HttpGet("password/resourceTemplate", Name = RouteDictionary.RegisterPasswordResourceTemplate)]
        public IActionResult RegisterPasswordResourceTemplate()
        {
            return Ok(_resourceBuilder.Build(new RegisterPasswordIdentityTemplate()));
        }

        [AllowAnonymous]
        [HttpPost("password", Name = RouteDictionary.RegisterPassword)]
        public IActionResult RegisterPassword(RegisterPasswordIdentityTemplate template)
        {
            PasswordIdentityAdto passwordIdentityAdto = _identityApplicationService.RegisterPassword(new RegisterPasswordAdto
            {
                Identifier = template.Identifier,
                Password = template.Password,
                ConfirmPassword = template.ConfirmPassword,
                EmailAddress = template.EmailAddress
            });

            return CreatedAtRoute(RouteDictionary.GetPasswordIdentity, new { passwordIdentityAdto.IdentityId }, _resourceBuilder.Build(new PasswordIdentityResource
            {
                IdentityId = passwordIdentityAdto.IdentityId,
                Identifier = passwordIdentityAdto.Identifier,
                Password = passwordIdentityAdto.Password,
                Version = passwordIdentityAdto.Version
            }));
        }

        [HttpPost("refreshToken", Name = RouteDictionary.CreateRefreshToken)]
        public IActionResult CreateRefreshToken()
        {
            RefreshTokenIdentityAdto refreshTokenIdentityAdto = _identityApplicationService.CreateRefreshToken(new CreateRefreshTokenAdto
            {
                IdentityId = _currentIdentityProvider.Id
            });

            return Ok(_resourceBuilder.Build(new RefreshTokenIdentityResource
            {
                Token = refreshTokenIdentityAdto.Token
            }));
        }
    }
}
