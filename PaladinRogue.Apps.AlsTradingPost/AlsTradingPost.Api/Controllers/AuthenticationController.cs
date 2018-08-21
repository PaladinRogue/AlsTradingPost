using System.Threading.Tasks;
using AlsTradingPost.Api.Authentication;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Setup.Infrastructure.Routing;
using AutoMapper;
using Common.Api.Builders.Resource;
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
        private readonly IMapper _mapper;

        public AuthenticationController(
            ISecure<IAuthenticationApplicationService> secureAuthenticationApplicationService,
            IResourceBuilder resourceBuilder,
            IMapper mapper)
        {
            _secureAuthenticationApplicationService = secureAuthenticationApplicationService;
            _resourceBuilder = resourceBuilder;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("services", Name = RouteDictionary.AuthenticationServices)]
        public IActionResult GetAuthenticationServices()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new AuthenticationServicesResource())
            );
        }

        [AllowAnonymous]
        [Route("login/resourceTemplate", Name = RouteDictionary.AuthenticationLoginTemplate)]
        public IActionResult GetAuthenticationTemplate()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new AuthenticationTemplate())
            );
        }

        [Route("login", Name = RouteDictionary.AuthenticationLogin)]
        public async Task<IActionResult> PostLogin([FromBody] AuthenticationTemplate template)
        {
            JwtAdto jwtAdto = await _secureAuthenticationApplicationService.Service.LoginAsync(new LoginAdto());

            return new ObjectResult(
                _resourceBuilder.Build(_mapper.Map<JwtAdto, JwtResource>(jwtAdto))
            );
        }

        [AllowAnonymous]
        [Route("refreshToken/resourceTemplate", Name = RouteDictionary.AuthenticationRefreshTokenTemplate)]
        public IActionResult GetRefreshTokenTemplate()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new RefreshTokenTemplate())
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
                _resourceBuilder.Build(_mapper.Map<JwtAdto, JwtResource>(jwt))
            );
        }
    }
}