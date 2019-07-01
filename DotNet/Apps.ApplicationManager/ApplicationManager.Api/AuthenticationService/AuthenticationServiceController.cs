using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationManager.ApplicationServices.AuthenticationServices;
using ApplicationManager.ApplicationServices.AuthenticationServices.Models;
using ApplicationManager.Setup.Infrastructure.Authorisation;
using AutoMapper;
using Common.Api.Builders.Resource;
using Common.Api.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationManager.Api.AuthenticationService
{
    [DefaultControllerRoute("authenticationServices")]
    [Authorize(Policies.User)]
    public class AuthenticationServiceController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly IAuthenticationServiceApplicationService _authenticationServiceApplicationService;

        private readonly IMapper _mapper;

        public AuthenticationServiceController(
            IResourceBuilder resourceBuilder,
            IAuthenticationServiceApplicationService authenticationServiceApplicationService,
            IMapper mapper)
        {
            _resourceBuilder = resourceBuilder;
            _authenticationServiceApplicationService = authenticationServiceApplicationService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("", Name = RouteDictionary.GetAuthenticationServices)]
        public IActionResult Get()
        {
            IEnumerable<AuthenticationServiceAdto> authenticationServiceAdtos = _authenticationServiceApplicationService.GetAuthenticationServices();

            return Ok(_resourceBuilder.Build(new AuthenticationServicesResource
            {
                Results = authenticationServiceAdtos.Select(a => new AuthenticationServiceSummaryResource
                {
                    Type = a.Type,
                    AccessUrl = a.AccessUrl
                }).ToList()
            }, new AuthenticationServiceSummaryResource()));
        }

        [HttpGet("resourceTemplate", Name = RouteDictionary.AuthenticationServiceResourceTemplate)]
        public IActionResult GetResetPasswordResourceTemplate()
        {
            return Ok(_resourceBuilder.Build(new AuthenticationServiceTemplate()));
        }

        [HttpPost("", Name = RouteDictionary.CreateAuthenticationService)]
        public IActionResult Post(AuthenticationServiceTemplate template)
        {
            ClientCredentialAdto clientCredentialAdto =
                _authenticationServiceApplicationService.CreateClientCredential(_mapper.Map<AuthenticationServiceTemplate, CreateClientCredentialAdto>(template));

            return CreatedAtRoute(RouteDictionary.GetAuthenticationService, new {clientCredentialAdto.Id},
                _resourceBuilder.Build(_mapper.Map<ClientCredentialAdto, AuthenticationServiceResource>(clientCredentialAdto)));
        }

        [HttpGet("{id}", Name = RouteDictionary.GetAuthenticationService)]
        public IActionResult Get(Guid id)
        {
            ClientCredentialAdto clientCredentialAdto =
                _authenticationServiceApplicationService.GetClientCredential(new GetClientCredentialAdto
                {
                    Id = id
                });

            return Ok(_resourceBuilder.Build(_mapper.Map<ClientCredentialAdto, AuthenticationServiceResource>(clientCredentialAdto)));
        }

        [HttpPut("{id}", Name = RouteDictionary.ChangeAuthenticationService)]
        public IActionResult Put(Guid id, AuthenticationServiceTemplate template)
        {
            ChangeClientCredentialAdto changeClientCredentialAdto = new ChangeClientCredentialAdto
            {
                Id = id,
                Name = template.Name,
                ClientId = template.ClientId,
                ClientSecret = template.ClientSecret,
                GrantAccessTokenUrl = template.GrantAccessTokenUrl,
                ValidateAccessTokenUrl = template.ValidateAccessTokenUrl,
                ClientGrantAccessTokenUrl = template.ClientGrantAccessTokenUrl,
                Version = template.Version
            };

            ClientCredentialAdto clientCredentialAdto =
                _authenticationServiceApplicationService.ChangeClientCredential(changeClientCredentialAdto);

            return Ok(_resourceBuilder.Build(_mapper.Map<ClientCredentialAdto, AuthenticationServiceResource>(clientCredentialAdto)));
        }
    }
}