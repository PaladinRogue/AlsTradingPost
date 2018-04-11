using System;
using System.Threading.Tasks;
using Authentication.Api.FacebookModels;
using Authentication.Api.Request;
using Authentication.Api.Resources;
using Authentication.Application.Identity.Interfaces;
using Authentication.Application.Identity.Models;
using Authentication.Setup.Settings;
using Common.Api.Authentication;
using Common.Api.Authentication.FacebookModels;
using Common.Api.Exceptions;
using Common.Api.Factories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Authentication.Api.Controllers
{
	[Route("api/[controller]")]
	public class FacebookAuthController : Controller
	{
		private readonly FacebookAuthSettings _fbAuthSettings;
		private readonly IJwtFactory _jwtFactory;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IIdentityApplicationService _identityApplicationService;
		private readonly ILogger<FacebookAuthController> _logger;
	    private readonly IEncryptionFactory _encryptionFactory;
	    private readonly JwtIssuerOptions _jwtIssuerOptions;

	    public FacebookAuthController(IOptions<FacebookAuthSettings> fbAuthSettingsAccessor,
	        IJwtFactory jwtFactory,
	        IHttpClientFactory httpClientFactory,
	        IIdentityApplicationService identityApplicationService,
	        ILogger<FacebookAuthController> logger,
	        IEncryptionFactory encryptionFactory,
	        IOptions<JwtIssuerOptions> jwtIssuerOptionsAccessor)
	    {
	        _fbAuthSettings = fbAuthSettingsAccessor.Value;
	        _jwtFactory = jwtFactory;
	        _httpClientFactory = httpClientFactory;
	        _identityApplicationService = identityApplicationService;
	        _logger = logger;
	        _encryptionFactory = encryptionFactory;
	        _jwtIssuerOptions = jwtIssuerOptionsAccessor.Value;
	    }

	    [HttpPost]
		public async Task<IActionResult> Post([FromBody] FacebookAuthRequestDto request)
		{
			var appAccessTokenResponse = 
				await _httpClientFactory.GetStringAsync(new Uri(string.Format(_fbAuthSettings.AccessTokenEndpoint, _fbAuthSettings.AppId, _fbAuthSettings.AppSecret)));
			FacebookAppAccessToken appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

			var userAccessTokenValidationResponse = 
				await _httpClientFactory.GetStringAsync(new Uri(string.Format(_fbAuthSettings.AccessTokenValidationEndpoint, request.AccessToken, appAccessToken.AccessToken)));
			FacebookUserAccessTokenValidation userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

			if (!userAccessTokenValidation.Data.IsValid)
			{
				_logger.LogInformation("Invalid facebook token.");
				throw new BadRequestException("Invalid facebook token.");
			}

			IdentityAdto identity = _identityApplicationService.Get(new GetIdentityAdto
			{
				AuthenticationId = userAccessTokenValidation.Data.UserId.ToString()
			});

		    FacebookJwtResource jwt = await _jwtFactory.GenerateJwt<FacebookJwtResource>(identity.Id);

		    jwt.AccessToken = _encryptionFactory.Enrypt(request.AccessToken, _jwtIssuerOptions.SigningKey);

            return new ObjectResult(jwt);
		}
	}
}