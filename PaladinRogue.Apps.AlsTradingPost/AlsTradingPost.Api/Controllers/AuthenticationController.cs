using System.Threading.Tasks;
using AlsTradingPost.Api.Authentication;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Setup.Infrastructure.Routing;
using AutoMapper;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Template;
using Common.Application.Authentication;
using Common.Application.Authorisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ISecure<IAuthenticationApplicationService> _secureAuthenticationApplicationService;
        private readonly IResourceBuilder _resourceBuilder;
        private readonly ITemplateBuilder _templateBuilder;
        private readonly IMapper _mapper;

        public AuthenticationController(
            ISecure<IAuthenticationApplicationService> secureAuthenticationApplicationService,
            ITemplateBuilder templateBuilder,
            IResourceBuilder resourceBuilder,
            IMapper mapper)
        {
            _secureAuthenticationApplicationService = secureAuthenticationApplicationService;
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
            JwtAdto jwtAdto = await _secureAuthenticationApplicationService.Service.LoginAsync(new LoginAdto());

            return new ObjectResult(
                _resourceBuilder.Create(_mapper.Map<JwtAdto, JwtResource>(jwtAdto))
                    .WithResourceMeta()
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
            JwtAdto jwt = await _secureAuthenticationApplicationService.Service.RefreshTokenAsync(
                new RefreshTokenAdto
                {
                    SessionId = template.SessionId,
                    Token = template.RefreshToken
                });

            return new ObjectResult(
                _resourceBuilder.Create(_mapper.Map<JwtAdto, JwtResource>(jwt))
                    .WithResourceMeta()
                    .Build()
            );
        }
    }
}