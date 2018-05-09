using System;
using System.Threading.Tasks;
using AlsTradingPost.Api.Authentication;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Setup.Infrastructure.Settings;
using AutoMapper;
using Common.Api.Authentication;
using Common.Api.Authentication.FacebookModels;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Template;
using Common.Api.HttpClient.Interfaces;
using Common.Application.Authentication;
using Common.Setup.Infrastructure.Authentication;
using Common.Setup.Infrastructure.Encryption.Interfaces;
using Common.Setup.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly JwtAuthenticationIssuerOptions _jwtAuthenticationIssuerOptions;
        private readonly FacebookSettings _fbSettings;
        private readonly IAuthenticationApplicationService _authenticationApplicationService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEncryptionFactory _encryptionFactory;
        private readonly IResourceTemplateBuilder _resourceTemplateBuilder;
        private readonly IResourceBuilder _resourceBuilder;
        private readonly ITemplateBuilder _templateBuilder;
        private readonly IMapper _mapper;

        public AuthenticationController(IOptions<FacebookSettings> fbSettingsAccessor,
            IOptions<JwtAuthenticationIssuerOptions> jwtAuthenticationIssuerOptions,
            IAuthenticationApplicationService authenticationApplicationService,
            IHttpClientFactory httpClientFactory,
            IEncryptionFactory encryptionFactory,
            IResourceTemplateBuilder resourceTemplateBuilder,
            ITemplateBuilder templateBuilder,
            IResourceBuilder resourceBuilder,
            IMapper mapper)
        {
            _authenticationApplicationService = authenticationApplicationService;
            _httpClientFactory = httpClientFactory;
            _encryptionFactory = encryptionFactory;
            _resourceTemplateBuilder = resourceTemplateBuilder;
            _templateBuilder = templateBuilder;
            _resourceBuilder = resourceBuilder;
            _mapper = mapper;
            _jwtAuthenticationIssuerOptions = jwtAuthenticationIssuerOptions.Value;
            _fbSettings = fbSettingsAccessor.Value;
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
        [Route("facebook/resourceTemplate", Name = RouteDictionary.AuthenticationFacebookTemplate)]
        public IActionResult GetAuthenticationTemplate()
        {
            return new ObjectResult(
                _templateBuilder.Create<AuthenticationTemplate>()
                    .WithTemplateMeta()
                    .Build()
            );
        }

        [Route("facebook", Name = RouteDictionary.AuthenticationFacebook)]
        public async Task<IActionResult> PostFacebook([FromBody] AuthenticationTemplate template)
        {
            string accessToken = _encryptionFactory.Decrypt<string>(template.AccessToken,
                _jwtAuthenticationIssuerOptions.SigningKey);

            string userAccessTokenValidationResponse;

            try
            {
                userAccessTokenValidationResponse = await _httpClientFactory.GetStringAsync(new Uri(
                    string.Format(_fbSettings.AccessTokenValidationEndpoint, accessToken, accessToken)
                ));
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Invalid facebook token.", ex);
            }

            FacebookUserAccessTokenValidation userAccessTokenValidation =
                JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                throw new BadRequestException("Invalid facebook token.");
            }

            string serializedUserData = await _httpClientFactory.GetStringAsync(new Uri(
                string.Format(_fbSettings.DataEndpoint, accessToken)
            ));

            FacebookUserData userData = JsonConvert.DeserializeObject<FacebookUserData>(serializedUserData);
            LoginAdto loginAdto = Mapper.Map<FacebookUserData, LoginAdto>(userData);

            JwtAdto jwtAdto = await _authenticationApplicationService.LoginAsync(loginAdto);

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