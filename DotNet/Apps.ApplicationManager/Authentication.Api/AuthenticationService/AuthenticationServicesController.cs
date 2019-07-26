using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.ApplicationServices.AuthenticationServices;
using Authentication.ApplicationServices.AuthenticationServices.Models;
using Authentication.Setup.Infrastructure.Authorisation;
using Authentication.Setup.Infrastructure.Routing;
using Common.Api.Builders.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.AuthenticationService
{
    [AuthenticationControllerRoute("")]
    [Authorize(Policies.User)]
    public class AuthenticationServicesController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly IAuthenticationServiceApplicationService _authenticationServiceApplicationService;

        public AuthenticationServicesController(
            IResourceBuilder resourceBuilder,
            IAuthenticationServiceApplicationService authenticationServiceApplicationService)
        {
            _resourceBuilder = resourceBuilder;
            _authenticationServiceApplicationService = authenticationServiceApplicationService;
        }

        [AllowAnonymous]
        [HttpGet("", Name = RouteDictionary.GetAuthenticationServices)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<AuthenticationServiceAdto> authenticationServiceAdtos = await _authenticationServiceApplicationService.GetAuthenticationServicesAsync();

            return Ok(_resourceBuilder.BuildCollection(new AuthenticationServicesResource
            {
                Results = authenticationServiceAdtos.Select<AuthenticationServiceAdto, AuthenticationServiceSummaryResource>(a =>
                {
                    switch (a)
                    {
                        case PasswordAuthenticationServiceAdto passwordAuthenticationServiceAdto:
                            return new PasswordAuthenticationServiceSummaryResource();
                        case RefreshTokenAuthenticationServiceAdto refreshTokenAuthenticationServiceAdto:
                            return new RefreshTokenAuthenticationServiceSummaryResource();
                        case ClientCredentialAuthenticationServiceAdto clientCredentialAuthenticationServiceAdto:
                            return new ClientCredentialAuthenticationServiceSummaryResource
                            {
                                Id = clientCredentialAuthenticationServiceAdto.Id,
                                AccessUrl = clientCredentialAuthenticationServiceAdto.AccessUrl,
                                Name = clientCredentialAuthenticationServiceAdto.Name
                            };
                        default:
                            throw new ArgumentOutOfRangeException(a.GetType().Name);
                    }
                }).ToList()
            }));
        }
    }
}