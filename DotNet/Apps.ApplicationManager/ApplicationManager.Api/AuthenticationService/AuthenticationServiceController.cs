using ApplicationManager.ApplicationServices.AuthenticationServices;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using ApplicationManager.Setup.Infrastructure.Authorisation;
using Common.Api.Builders.Resource;
using Common.Api.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationManager.Api.AuthenticationService
{
    [DefaultControllerRoute]
    [Authorize(Policies.User)]
    public class AuthenticationServiceController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly IAuthenticationServiceApplicationService _authenticationServiceApplicationService;

        public AuthenticationServiceController(
            IResourceBuilder resourceBuilder,
            IAuthenticationServiceApplicationService authenticationServiceApplicationService)
        {
            _resourceBuilder = resourceBuilder;
            _authenticationServiceApplicationService = authenticationServiceApplicationService;
        }

        [HttpGet("resourceTemplate", Name = RouteDictionary.AuthenticationServiceResourceTemplate)]
        public IActionResult GetResetPasswordResourceTemplate()
        {
            return Ok(_resourceBuilder.Build(new AuthenticationServiceTemplate()));
        }

        [HttpPost("", Name = RouteDictionary.CreateAuthenticationService)]
        public IActionResult Post(AuthenticationServiceTemplate template)
        {
            ClientCredentialAdto clientCredentialAdto = _authenticationServiceApplicationService.CreateClientCredential(new CreateClientCredentialAdto
            {
                Name = template.Name,
                ClientId = template.ClientId,
                ClientSecret = template.ClientSecret,
                GrantAccessTokenUrl = template.GrantAccessTokenUrl,
                ValidateAccessTokenUrl = template.ValidateAccessTokenUrl,
                ClientGrantAccessTokenUrl = template.ClientGrantAccessTokenUrl
            });

            return CreatedAtRoute(RouteDictionary.GetAuthenticationService, new { clientCredentialAdto.Id }, _resourceBuilder.Build(new AuthenticationServiceResource
            {
                Id = clientCredentialAdto.Id,
                Name = clientCredentialAdto.Name,
                ClientId = clientCredentialAdto.ClientId,
                ClientSecret = clientCredentialAdto.ClientSecret,
                GrantAccessTokenUrl = clientCredentialAdto.GrantAccessTokenUrl,
                ValidateAccessTokenUrl = clientCredentialAdto.ValidateAccessTokenUrl,
                ClientGrantAccessTokenUrl = clientCredentialAdto.ClientGrantAccessTokenUrl,
                Version = clientCredentialAdto.Version
            }));
        }
    }
}
