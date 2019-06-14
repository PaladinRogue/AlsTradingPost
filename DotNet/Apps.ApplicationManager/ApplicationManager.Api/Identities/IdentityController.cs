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
        [HttpGet("{identityId}/password/resourceTemplate", Name = RouteDictionary.IdentityPasswordResourceTemplate)]
        public IActionResult Get(Guid identityId, [FromQuery]string token)
        {
            IdentityAdto identityAdto = _identityApplicationService.Get(new GetIdentityAdto
            {
                Id = identityId
            });

            BuiltResource builtResource = _resourceBuilder.Build(new CreatePasswordIdentityTemplate
            {
                Token = token,
                Version = identityAdto.Version
            });

            return Ok(builtResource);
        }

        [AllowAnonymous]
        [HttpPost("{identityId}/password", Name = RouteDictionary.IdentityPassword)]
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

            return CreatedAtRoute(RouteDictionary.IdentityPassword, new { identityId }, passwordIdentityAdto);
        }
    }
}
