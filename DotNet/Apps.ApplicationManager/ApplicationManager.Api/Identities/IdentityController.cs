using System;
using ApplicationManager.ApplicationServices.Identities;
using ApplicationManager.ApplicationServices.Identities.Models;
using Common.Api.Authentication;
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

        private readonly IIdentityApplicationService _identityApplicationService;

        public IdentityController(
            IResourceBuilder resourceBuilder,
            IIdentityApplicationService identityApplicationService)
        {
            _resourceBuilder = resourceBuilder;
            _identityApplicationService = identityApplicationService;
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

        [HttpGet("{identityId}/password", Name = RouteDictionary.GetPasswordIdentity)]
        public IActionResult GetPasswordIdentity(Guid identityId)
        {
            PasswordIdentityAdto passwordIdentityAdto = _identityApplicationService.GetPasswordIdentity(new GetPasswordIdentityAdto
            {
                IdentityId = identityId
            });

            return Ok(_resourceBuilder.Build(new PasswordIdentityResource
            {
                Identifier = passwordIdentityAdto.Identifier,
                Password = passwordIdentityAdto.Password,
                Version = passwordIdentityAdto.Version
            }));
        }

        [HttpGet("{identityId}/password/change/resourceTemplate", Name = RouteDictionary.ChangePasswordResourceTemplate)]
        public IActionResult ChangePasswordResourceTemplate(Guid identityId)
        {
            IdentityAdto identityAdto = _identityApplicationService.Get(new GetIdentityAdto
            {
                Id = identityId
            });

            return Ok(_resourceBuilder.Build(new ChangePasswordIdentityTemplate
            {
                Version = identityAdto.Version
            }));
        }

        [HttpPost("{identityId}/password/change", Name = RouteDictionary.ChangePassword)]
        public IActionResult ChangePassword(Guid identityId, ChangePasswordIdentityTemplate template)
        {
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
        [HttpGet("{identityId}/confirm/resourceTemplate", Name = RouteDictionary.ConfirmIdentityResourceTemplate)]
        public IActionResult ConfirmIdentityResourceTemplate(Guid identityId, [FromQuery]string token)
        {
            IdentityAdto identityAdto = _identityApplicationService.Get(new GetIdentityAdto
            {
                Id = identityId,
            });

            return Ok(_resourceBuilder.Build(new ConfirmIdentityTemplate
            {
                Version = identityAdto.Version,
                Token = token
            }));
        }

        [AllowRestrictedAppAccess]
        [HttpPost("{identityId}/confirm", Name = RouteDictionary.ConfirmIdentity)]
        public IActionResult ChangePassword(Guid identityId, ConfirmIdentityTemplate template)
        {
            _identityApplicationService.ConfirmIdentity(new ConfirmIdentityAdto
            {
                IdentityId = identityId,
                Token = template.Token,
                Version = template.Version
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
    }
}
