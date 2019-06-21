using System;
using ApplicationManager.ApplicationServices.Identities;
using ApplicationManager.ApplicationServices.Identities.Models;
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
        [HttpGet("{identityId}/password/resourceTemplate", Name = RouteDictionary.PasswordIdentityResourceTemplate)]
        public IActionResult GetPasswordIdentityResourceTemplate(Guid identityId, [FromQuery]string token)
        {
            IdentityAdto identityAdto = _identityApplicationService.Get(new GetIdentityAdto
            {
                Id = identityId
            });

            return Ok(_resourceBuilder.Build(new CreatePasswordIdentityTemplate
            {
                Token = token,
                Version = identityAdto.Version
            }));
        }

        [AllowAnonymous]
        [HttpPost("{identityId}/password", Name = RouteDictionary.PostPasswordIdentity)]
        public IActionResult Post(Guid identityId, CreatePasswordIdentityTemplate template)
        {
            PasswordIdentityAdto passwordIdentityAdto = _identityApplicationService.CreateConfirmedPasswordIdentity(new CreateConfirmedPasswordIdentityAdto
            {
                IdentityId = identityId,
                Identifier = template.Identifier,
                Password = template.Password,
                ConfirmPassword = template.ConfirmPassword,
                Token = template.Token,
                Version = template.Version
            });

            return CreatedAtRoute(RouteDictionary.GetPasswordIdentity, new { identityId }, _resourceBuilder.Build(new PasswordIdentityResource
            {
                Identifier = passwordIdentityAdto.Identifier,
                Password = passwordIdentityAdto.Password,
                Version = passwordIdentityAdto.Version
            }));
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

            return Ok(_resourceBuilder.Build(new PasswordIdentityResource
            {
                Identifier = passwordIdentityAdto.Identifier,
                Password = passwordIdentityAdto.Password,
                Version = passwordIdentityAdto.Version
            }));
        }
    }
}
