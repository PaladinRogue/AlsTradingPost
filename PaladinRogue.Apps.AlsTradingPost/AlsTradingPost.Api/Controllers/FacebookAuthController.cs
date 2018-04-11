using System.Threading.Tasks;
using AlsTradingPost.Api.Request.FacebookAuth;
using AlsTradingPost.Application.User.Interfaces;
using AlsTradingPost.Application.User.Models;
using AlsTradingPost.Setup.Settings;
using Common.Api.Authentication;
using Common.Api.Factories.Interfaces;
using Common.Api.Providers.Interfaces;
using Common.Api.Resource;
using Common.Api.Resource.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    public class FacebookAuthController : Controller
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly ILogger<FacebookAuthController> _logger;
        private readonly IClaimsFactory _claimsFactory;
        private readonly IIdentityProvider _identityProvider;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly FacebookSettings _fbSettings;
        private readonly IUserApplicationService _userApplicationService;

        public FacebookAuthController(IOptions<FacebookSettings> fbSettingsAccessor, IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptionsAccessor, ILogger<FacebookAuthController> logger,
            IClaimsFactory claimsFactory, IIdentityProvider identityProvider, IUserApplicationService userApplicationService)
        {
            _jwtFactory = jwtFactory;
            _logger = logger;
            _claimsFactory = claimsFactory;
            _identityProvider = identityProvider;
            _userApplicationService = userApplicationService;
            _jwtOptions = jwtOptionsAccessor.Value;
            _fbSettings = fbSettingsAccessor.Value;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FacebookAuthRequestDto request)
        {
            //            var userAccessTokenValidationResponse =
            //                await _httpClientFactory.GetStringAsync(new Uri(string.Format(_fbAuthSettings.AccessTokenValidationEndpoint, request.AccessToken, appAccessToken.AccessToken)));
            //            FacebookUserAccessTokenValidation userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);
            //
            //            if (!userAccessTokenValidation.Data.IsValid)
            //            {
            //                _logger.LogInformation("Invalid facebook token.");
            //                throw new BadRequestException("Invalid facebook token.");
            //            }
            //
            //            IdentityAdto identity = _identityApplicationService.Get(new GetIdentityAdto
            //            {
            //                AuthenticationId = userAccessTokenValidation.Data.UserId.ToString()
            //            });

            //            IJwtResource jwt = await _jwtFactory.GenerateJwt<JwtResource>(_identityProvider.Id);

            return new ObjectResult("Ok");
        }
    }
}