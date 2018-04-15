using System;
using System.Threading.Tasks;
using AlsTradingPost.Api.FacebookAuth;
using AlsTradingPost.Application.UserApplication.Interfaces;
using AlsTradingPost.Application.UserApplication.Models;
using AlsTradingPost.Resources.Claims;
using AlsTradingPost.Setup.Infrastructure.Settings;
using AutoMapper;
using Common.Api.Authentication;
using Common.Api.Authentication.Constants;
using Common.Api.Authentication.FacebookModels;
using Common.Api.Authentication.Interfaces;
using Common.Api.Encryption.Interfaces;
using Common.Api.Exceptions;
using Common.Api.HttpClient.Interfaces;
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
            string accessToken =
                _encryptionFactory.Decrypt<string>(request.AccessToken, _jwtAuthenticationIssuerOptions.SigningKey);

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
            FacebookUpdateAdto facebookUpdateAdto = Mapper.Map<FacebookUserData, FacebookUpdateAdto>(userData);

            UserAdto userAdto = _userApplicationService.FacebookUpdate(facebookUpdateAdto);

            IJwtResource jwt = await _jwtFactory.GenerateJwt<JwtResource>(
                ClaimsBuilder.CreateBuilder().WithPersonas(userAdto.Personas).WithSubject(userAdto.Id).WithRole(JwtClaims.AppAccess).Build()
            );

            return new ObjectResult(jwt);
        }
    }
}