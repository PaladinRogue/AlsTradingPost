﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaladinRogue.Authentication.Application.AuthenticationServices;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models;
using PaladinRogue.Authentication.Setup.Infrastructure.Authorisation;
using PaladinRogue.Authentication.Setup.Infrastructure.Routing;
using PaladinRogue.Library.Core.Api.Builders.Resource;

namespace PaladinRogue.Authentication.Api.AuthenticationService
{
    [AuthenticationControllerRoute("")]
    [Authorize(Policies.User)]
    public class AuthenticationServicesController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly IAuthenticationServiceApplicationService _authenticationServiceApplicationService;

        private readonly IMapper _mapper;

        public AuthenticationServicesController(
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
        public async Task<IActionResult> Get([FromQuery]string redirectUri,
            [FromQuery]string state)
        {
            IEnumerable<AuthenticationServiceAdto> authenticationServiceAdtos = await _authenticationServiceApplicationService.GetAuthenticationServicesAsync(new GetAuthenticationServicesAdto
            {
                RedirectUri = redirectUri,
                State = state
            });

            return Ok(_resourceBuilder.BuildCollection(new AuthenticationServicesResource
            {
                Results = _mapper.Map<IEnumerable<AuthenticationServiceAdto>, IList<AuthenticationServiceSummaryResource>>(authenticationServiceAdtos)
            }));
        }
    }
}