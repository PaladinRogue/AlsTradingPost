using System;
using System.Threading.Tasks;
using AlsTradingPost.Api.FacebookAuth;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Setup.Infrastructure.Settings;
using AutoMapper;
using Common.Api.Authentication;
using Common.Api.Authentication.FacebookModels;
using Common.Api.Builders.Resource;
using Common.Api.HttpClient.Interfaces;
using Common.Application.Authentication;
using Common.Application.Encryption.Interfaces;
using Common.Setup.Infrastructure.Authentication;
using Common.Setup.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly JwtAuthenticationIssuerOptions _jwtAuthenticationIssuerOptions;
        private readonly FacebookSettings _fbSettings;
        private readonly IAuthenticationApplicationService _authenticationApplicationService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEncryptionFactory _encryptionFactory;
        private readonly IResourceTemplateBuilder _resourceTemplateBuilder;

        public AuthenticationController(IOptions<FacebookSettings> fbSettingsAccessor,
            IOptions<JwtAuthenticationIssuerOptions> jwtAuthenticationIssuerOptions,
            ILogger<AuthenticationController> logger,
            IAuthenticationApplicationService authenticationApplicationService,
            IHttpClientFactory httpClientFactory,
            IEncryptionFactory encryptionFactory,
            IResourceTemplateBuilder resourceTemplateBuilder)
        {
            _logger = logger;
            _authenticationApplicationService = authenticationApplicationService;
            _httpClientFactory = httpClientFactory;
            _encryptionFactory = encryptionFactory;
            _resourceTemplateBuilder = resourceTemplateBuilder;
            _jwtAuthenticationIssuerOptions = jwtAuthenticationIssuerOptions.Value;
            _fbSettings = fbSettingsAccessor.Value;
        }

        [Route("facebook", Name = RouteDictionary.AuthenticationFacebook)]
        public async Task<IActionResult> PostFacebook([FromBody] FacebookAuthTemplate template)
        {
            string accessToken =
                _encryptionFactory.Decrypt<string>(template.AccessToken, _jwtAuthenticationIssuerOptions.SigningKey);

            string userAccessTokenValidationResponse;

            try
            {
                userAccessTokenValidationResponse = await _httpClientFactory.GetStringAsync(new Uri(
                    string.Format(_fbSettings.AccessTokenValidationEndpoint, accessToken, accessToken)
                ));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Invalid facebook token.");
                throw new BadRequestException("Invalid facebook token.");
            }

            FacebookUserAccessTokenValidation userAccessTokenValidation =
                JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                _logger.LogInformation("Invalid facebook token.");
                throw new BadRequestException("Invalid facebook token.");
            }

            string serializedUserData = await _httpClientFactory.GetStringAsync(new Uri(
                string.Format(_fbSettings.DataEndpoint, accessToken)
            ));

            FacebookUserData userData = JsonConvert.DeserializeObject<FacebookUserData>(serializedUserData);
            LoginAdto loginAdto = Mapper.Map<FacebookUserData, LoginAdto>(userData);

            JwtAdto jwtAdto = await _authenticationApplicationService.LoginAsync(loginAdto);

            return new ObjectResult(
                _resourceTemplateBuilder.Create(Mapper.Map<JwtAdto, JwtResource>(jwtAdto), template)
                    .WithResourceMeta()
                    .WithTemplateMeta()
                    .Build()
            );
        }
    }
}