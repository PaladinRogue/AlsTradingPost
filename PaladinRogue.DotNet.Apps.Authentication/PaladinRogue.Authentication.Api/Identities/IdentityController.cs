using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaladinRogue.Authentication.Application.Identities;
using PaladinRogue.Authentication.Application.Identities.Models;
using PaladinRogue.Authentication.Setup.Infrastructure.Routing;
using PaladinRogue.Library.Core.Api.Authentication;
using PaladinRogue.Library.Core.Api.Builders.Resource;
using PaladinRogue.Library.Core.Setup.Infrastructure.Authorisation;

namespace PaladinRogue.Authentication.Api.Identities
{
    [AuthenticationControllerRoute]
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
        [HttpGet("", Name = RouteDictionary.GetIdentity)]
        public async Task<IActionResult> Get()
        {
            IdentityAdto identityAdto = await _identityApplicationService.GetAsync(new GetIdentityAdto
            {
                Id = _currentIdentityProvider.Id
            });

            switch (identityAdto)
            {
                case PasswordIdentityAdto passwordIdentityAdto:
                    return Ok(_resourceBuilder.Build(new PasswordIdentityResource
                    {
                        Id = passwordIdentityAdto.Id
                    }));
                case IdentityAdto defaultIdentityAdto:
                    return Ok(_resourceBuilder.Build(new IdentityResource
                    {
                        Id = defaultIdentityAdto.Id
                    }));
                default:
                    throw new ArgumentOutOfRangeException(nameof(identityAdto));
            }
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
        public async Task<IActionResult> Post(ResetPasswordTemplate template)
        {
            await _identityApplicationService.ResetPasswordAsync(new ResetPasswordAdto
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
        public async Task<IActionResult> Post(ForgotPasswordTemplate template)
        {
            await _identityApplicationService.ForgotPasswordAsync(new ForgotPasswordAdto
            {
                EmailAddress = template.EmailAddress
            });

            return Accepted(_resourceBuilder.Build(new ForgotPasswordResource()));
        }

        [AllowRestrictedAppAccess]
        [HttpGet("password/change/resourceTemplate", Name = RouteDictionary.ChangePasswordResourceTemplate)]
        public async Task<IActionResult> ChangePasswordResourceTemplate()
        {
            IdentityAdto identityAdto = await _identityApplicationService.GetAsync(new GetIdentityAdto
            {
                Id = _currentIdentityProvider.Id
            });

            return Ok(_resourceBuilder.Build(new ChangePasswordIdentityTemplate
            {
                Version = identityAdto.Version
            }));
        }

        [AllowRestrictedAppAccess]
        [HttpPost("password/change", Name = RouteDictionary.ChangePassword)]
        public async Task<IActionResult> ChangePassword(ChangePasswordIdentityTemplate template)
        {
            Guid identityId = _currentIdentityProvider.Id;

            PasswordAdto passwordAdto = await _identityApplicationService.ChangePasswordAsync(new ChangePasswordAdto
            {
                IdentityId = identityId,
                Password = template.Password,
                ConfirmPassword = template.ConfirmPassword,
                Version = template.Version
            });

            return Accepted(_resourceBuilder.Build(new ChangePasswordResource
            {
                Id = passwordAdto.IdentityId
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
        public async Task<IActionResult> ConfirmIdentity(ConfirmIdentityTemplate template)
        {
            await _identityApplicationService.ConfirmIdentityAsync(new ConfirmIdentityAdto
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
        public async Task<IActionResult> RegisterPassword(RegisterPasswordIdentityTemplate template)
        {
            PasswordAdto passwordAdto = await _identityApplicationService.RegisterPasswordAsync(new RegisterPasswordAdto
            {
                Identifier = template.Identifier,
                Password = template.Password,
                ConfirmPassword = template.ConfirmPassword,
                EmailAddress = template.EmailAddress
            });

            return Accepted(_resourceBuilder.Build(new RegisterPasswordResource
            {
                Id = passwordAdto.IdentityId
            }));
        }

        [HttpPost("refreshToken", Name = RouteDictionary.CreateRefreshToken)]
        public async Task<IActionResult> CreateRefreshToken()
        {
            RefreshTokenIdentityAdto refreshTokenIdentityAdto = await _identityApplicationService.CreateRefreshTokenAsync(new CreateRefreshTokenAdto
            {
                IdentityId = _currentIdentityProvider.Id
            });

            return Ok(_resourceBuilder.Build(new RefreshTokenResource
            {
                Token = refreshTokenIdentityAdto.Token
            }));
        }

        [AllowRestrictedAppAccess]
        [HttpPost("resendConfirm", Name = RouteDictionary.ResendConfirmIdentity)]
        public async Task<IActionResult> ResendConfirmIdentity()
        {
            await _identityApplicationService.ResendConfirmIdentityAsync(new ResendConfirmIdentityAdto
            {
                IdentityId = _currentIdentityProvider.Id
            });

            return Accepted(_resourceBuilder.Build(new ResendConfirmIdentityResource()));
        }

        [HttpPost("logout", Name = RouteDictionary.Logout)]
        public async Task<IActionResult> Logout()
        {
            await _identityApplicationService.LogoutAsync(new LogoutAdto
            {
                IdentityId = _currentIdentityProvider.Id
            });

            return NoContent();
        }
    }
}
