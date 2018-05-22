﻿using System.Threading.Tasks;
using AlsTradingPost.Api.Authentication;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using AutoMapper;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Template;
using Common.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationApplicationService _authenticationApplicationService;
        private readonly IResourceTemplateBuilder _resourceTemplateBuilder;
        private readonly IResourceBuilder _resourceBuilder;
        private readonly ITemplateBuilder _templateBuilder;
        private readonly IMapper _mapper;

        public AuthenticationController(
            IAuthenticationApplicationService authenticationApplicationService,
            IResourceTemplateBuilder resourceTemplateBuilder,
            ITemplateBuilder templateBuilder,
            IResourceBuilder resourceBuilder,
            IMapper mapper)
        {
            _authenticationApplicationService = authenticationApplicationService;
            _resourceTemplateBuilder = resourceTemplateBuilder;
            _templateBuilder = templateBuilder;
            _resourceBuilder = resourceBuilder;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("services", Name = RouteDictionary.AuthenticationServices)]
        public IActionResult GetAuthenticationServices()
        {
            return new ObjectResult(
                _resourceBuilder.Create(new AuthenticationServicesResource())
                    .Build()
            );
        }

        [AllowAnonymous]
        [Route("login/resourceTemplate", Name = RouteDictionary.AuthenticationLoginTemplate)]
        public IActionResult GetAuthenticationTemplate()
        {
            return new ObjectResult(
                _templateBuilder.Create<AuthenticationTemplate>()
                    .WithTemplateMeta()
                    .Build()
            );
        }

        [Route("login", Name = RouteDictionary.AuthenticationLogin)]
        public async Task<IActionResult> PostLogin([FromBody] AuthenticationTemplate template)
        {
            JwtAdto jwtAdto = await _authenticationApplicationService.LoginAsync(new LoginAdto());

            return new ObjectResult(
                _resourceTemplateBuilder.Create(_mapper.Map<JwtAdto, JwtResource>(jwtAdto), template)
                    .WithResourceMeta()
                    .WithTemplateMeta()
                    .Build()
            );
        }

        [AllowAnonymous]
        [Route("refreshToken/resourceTemplate", Name = RouteDictionary.AuthenticationRefreshTokenTemplate)]
        public IActionResult GetRefreshTokenTemplate()
        {
            return new ObjectResult(
                _templateBuilder.Create<RefreshTokenTemplate>()
                    .WithTemplateMeta()
                    .Build()
            );
        }

        [AllowAnonymous]
        [Route("refreshToken", Name = RouteDictionary.AuthenticationRefreshToken)]
        public async Task<IActionResult> PostRefreshToken([FromBody] RefreshTokenTemplate template)
        {
            JwtAdto jwt = await _authenticationApplicationService.RefreshTokenAsync(
                new RefreshTokenAdto
                {
                    SessionId = template.SessionId,
                    Token = template.RefreshToken
                });

            return new ObjectResult(
                _resourceTemplateBuilder.Create(_mapper.Map<JwtAdto, JwtResource>(jwt), template)
                    .WithResourceMeta()
                    .WithTemplateMeta()
                    .Build()
            );
        }
    }
}