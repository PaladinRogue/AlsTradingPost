﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.ApplicationServices.AuthenticationServices;
using Authentication.ApplicationServices.AuthenticationServices.Models;
using Authentication.Setup.Infrastructure.Authorisation;
using Authentication.Setup.Infrastructure.Routing;
using AutoMapper;
using Common.Api.Builders.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.AuthenticationService
{
    [AuthenticationControllerRoute("authenticationServices")]
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
                            return new PasswordAuthenticationServiceSummaryResource
                            {
                                Type = passwordAuthenticationServiceAdto.Type
                            };
                        case ClientCredentialAuthenticationServiceAdto clientCredentialAuthenticationServiceAdto:
                            return new ClientCredentialAuthenticationServiceSummaryResource
                            {
                                Id = clientCredentialAuthenticationServiceAdto.Id,
                                Type = clientCredentialAuthenticationServiceAdto.Type,
                                AccessUrl = clientCredentialAuthenticationServiceAdto.AccessUrl
                            };
                        default:
                            throw new ArgumentOutOfRangeException(a.GetType().Name);
                    }
                }).ToList()
            }));
        }

        [HttpGet("resourceTemplate", Name = RouteDictionary.AuthenticationServiceResourceTemplate)]
        public IActionResult GetResetPasswordResourceTemplate()
        {
            return Ok(_resourceBuilder.Build(new AuthenticationServiceTemplate()));
        }

        [HttpPost("", Name = RouteDictionary.CreateAuthenticationService)]
        public async Task<IActionResult> Post(AuthenticationServiceTemplate template)
        {
            ClientCredentialAdto clientCredentialAdto = await
                _authenticationServiceApplicationService.CreateClientCredential(_mapper.Map<AuthenticationServiceTemplate, CreateClientCredentialAdto>(template));

            return CreatedAtRoute(RouteDictionary.GetAuthenticationService, new {clientCredentialAdto.Id},
                _resourceBuilder.Build(_mapper.Map<ClientCredentialAdto, AuthenticationServiceResource>(clientCredentialAdto)));
        }

        [HttpGet("{id}", Name = RouteDictionary.GetAuthenticationService)]
        public async Task<IActionResult> Get(Guid id)
        {
            ClientCredentialAdto clientCredentialAdto = await
                _authenticationServiceApplicationService.GetClientCredentialAsync(new GetClientCredentialAdto
                {
                    Id = id
                });

            return Ok(_resourceBuilder.Build(_mapper.Map<ClientCredentialAdto, AuthenticationServiceResource>(clientCredentialAdto)));
        }

        [HttpPut("{id}", Name = RouteDictionary.ChangeAuthenticationService)]
        public async Task<IActionResult> Put(Guid id,
            AuthenticationServiceResource resource)
        {
            ChangeClientCredentialAdto changeClientCredentialAdto = new ChangeClientCredentialAdto
            {
                Id = id,
                Name = resource.Name,
                ClientId = resource.ClientId,
                ClientSecret = resource.ClientSecret,
                GrantAccessTokenUrl = resource.GrantAccessTokenUrl,
                ValidateAccessTokenUrl = resource.ValidateAccessTokenUrl,
                ClientGrantAccessTokenUrl = resource.ClientGrantAccessTokenUrl,
                AppAccessToken = resource.AppAccessToken,
                Version = resource.Version
            };

            ClientCredentialAdto clientCredentialAdto = await
                _authenticationServiceApplicationService.ChangeClientCredentialAsync(changeClientCredentialAdto);

            return Ok(_resourceBuilder.Build(_mapper.Map<ClientCredentialAdto, AuthenticationServiceResource>(clientCredentialAdto)));
        }
    }
}