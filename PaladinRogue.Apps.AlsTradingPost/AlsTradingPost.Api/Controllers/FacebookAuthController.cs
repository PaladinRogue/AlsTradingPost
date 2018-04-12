using System;
using System.Threading.Tasks;
using AlsTradingPost.Api.Templates.FacebookAuth;
using AlsTradingPost.Application.User.Interfaces;
using AlsTradingPost.Application.User.Models;
using AlsTradingPost.Setup.Settings;
using AutoMapper;
using Common.Api.Authentication;
using Common.Api.Authentication.FacebookModels;
using Common.Api.Exceptions;
using Common.Api.Factories.Interfaces;
using Common.Api.Resource;
using Common.Api.Resource.Interfaces;
using Common.Resources.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    public class FacebookAuthController : Controller
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly ILogger<FacebookAuthController> _logger;
        private readonly JwtAuthenticationIssuerOptions _jwtAuthenticationIssuerOptions;
        private readonly FacebookSettings _fbSettings;
        private readonly IUserApplicationService _userApplicationService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEncryptionFactory _encryptionFactory;

        public FacebookAuthController(IOptions<FacebookSettings> fbSettingsAccessor,
            IJwtFactory jwtFactory,
            IOptions<JwtAuthenticationIssuerOptions> jwtAuthenticationIssuerOptions,
            ILogger<FacebookAuthController> logger,
            IUserApplicationService userApplicationService,
            IHttpClientFactory httpClientFactory,
            IEncryptionFactory encryptionFactory)
        {
            _jwtFactory = jwtFactory;
            _logger = logger;
            _userApplicationService = userApplicationService;
            _httpClientFactory = httpClientFactory;
            _encryptionFactory = encryptionFactory;
            _jwtAuthenticationIssuerOptions = jwtAuthenticationIssuerOptions.Value;
            _fbSettings = fbSettingsAccessor.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FacebookAuthTemplate request)
        {
            var accessToken = _encryptionFactory.Decrypt<string>(request.AccessToken, _jwtAuthenticationIssuerOptions.SigningKey);

            string userAccessTokenValidationResponse;

            try
            {
                userAccessTokenValidationResponse = await _httpClientFactory.GetStringAsync(new Uri(
                    string.Format(_fbSettings.AccessTokenValidationEndpoint, accessToken, accessToken)
                ));
            }
            catch (Exception)
            {
                _logger.LogInformation("Invalid facebook token.");
                throw new BadRequestException("Invalid facebook token.");
            }

            FacebookUserAccessTokenValidation userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                _logger.LogInformation("Invalid facebook token.");
                throw new BadRequestException("Invalid facebook token.");
            }

            var serializedUserData = await _httpClientFactory.GetStringAsync(new Uri(
                string.Format(_fbSettings.DataEndpoint, accessToken)
            ));

            FacebookUserData userData = JsonConvert.DeserializeObject<FacebookUserData>(serializedUserData);
            FacebookUpdateAdto facebookUpdateAdto = Mapper.Map<FacebookUserData, FacebookUpdateAdto>(userData);

            Guid userId = _userApplicationService.FacebookUpdate(facebookUpdateAdto);

            IJwtResource jwt = await _jwtFactory.GenerateJwt<JwtResource>(userId);

            return new ObjectResult(jwt);
        }
    }
}