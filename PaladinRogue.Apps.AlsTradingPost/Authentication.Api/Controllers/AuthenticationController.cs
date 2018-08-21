using System;
using System.Threading.Tasks;
using Authentication.Api.Authentication;
using Authentication.Api.Authentication.FacebookModels;
using Authentication.Application.Authentication.Interfaces;
using Authentication.Application.Authentication.Models;
using Authentication.Setup.Settings;
using AutoMapper;
using Common.Api.Authentication.FacebookModels;
using Common.Api.Builders.Resource;
using Common.Api.HttpClient.Interfaces;
using Common.Application.Authentication;
using Common.Application.Authorisation;
using Common.Setup.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly FacebookAuthSettings _fbAuthSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISecure<IAuthenticationApplicationService> _secureAuthenticationApplicationService;
        private readonly IResourceBuilder _resourceBuilder;
        private readonly IMapper _mapper;

        public AuthenticationController(IOptions<FacebookAuthSettings> fbAuthSettingsAccessor,
            IJwtFactory jwtFactory,
            IHttpClientFactory httpClientFactory,
            IAuthenticationApplicationService authenticationApplicationService,
            IResourceBuilder resourceBuilder,
            IMapper mapper,
            ISecure<IAuthenticationApplicationService> secureAuthenticationApplicationService)
        {
            _fbAuthSettings = fbAuthSettingsAccessor.Value;
            _httpClientFactory = httpClientFactory;
            _resourceBuilder = resourceBuilder;
            _mapper = mapper;
            _secureAuthenticationApplicationService = secureAuthenticationApplicationService;
        }

        [Route("services", Name = RouteDictionary.AuthenticationServices)]
        public IActionResult GetAuthenticationServices()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new AuthenticationServicesResource())
            );
        }

        [Route("facebook/resourceTemplate", Name = RouteDictionary.AuthenticationFacebookTemplate)]
        public IActionResult GetAuthenticationTemplate()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new AuthenticationTemplate())
            );
        }

        [Route("facebook", Name = RouteDictionary.AuthenticationFacebook)]
        public async Task<IActionResult> PostFacebook([FromBody] AuthenticationTemplate template)
        {
            string appAccessTokenResponse = await _httpClientFactory.GetStringAsync(new Uri(string.Format(
                    _fbAuthSettings.AccessTokenEndpoint,
                    _fbAuthSettings.AppId,
                    _fbAuthSettings.AppSecret
                ))
            );

            FacebookAppAccessToken appAccessToken =
                JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

            string userAccessTokenValidationResponse = await _httpClientFactory.GetStringAsync(new Uri(string.Format(
                    _fbAuthSettings.AccessTokenValidationEndpoint,
                    template.AccessToken,
                    appAccessToken.AccessToken
                ))
            );

            FacebookUserAccessTokenValidation userAccessTokenValidation =
                JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                throw new BadRequestException("Invalid facebook token.");
            }

            ExtendedJwtAdto extendedJwt = await _secureAuthenticationApplicationService.Service.LoginAsync(
                new LoginAdto
                {
                    AuthenticationId = userAccessTokenValidation.Data.UserId.ToString(),
                    AccessToken = template.AccessToken
                });

            return new ObjectResult(
                _resourceBuilder.Build(_mapper.Map<ExtendedJwtAdto, FacebookJwtResource>(extendedJwt))
            );
        }

        [Route("refreshToken/resourceTemplate", Name = RouteDictionary.AuthenticationRefreshTokenTemplate)]
        public IActionResult GetRefreshTokenTemplate()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new RefreshTokenTemplate())
            );
        }

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