using System.Threading.Tasks;
using Authentication.Api.FacebookModels;
using Authentication.Api.Factories.Interfaces;
using Authentication.Api.Request;
using Authentication.Application.Identity.Interfaces;
using Authentication.Application.Identity.Models;
using Authentication.Setup.Settings;
using Common.Api.Authentication;
using Common.Api.Exceptions;
using Common.Api.Factories.Interfaces;
using Common.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Authentication.Api.Controllers
{
	[Route("api/[controller]")]
	public class FacebookAuthController : Controller
	{
		private readonly FacebookAuthSettings _fbAuthSettings;
		private readonly IJwtFactory _jwtFactory;
		private readonly IClaimsFactory _claimsFactory;
		private readonly JwtIssuerOptions _jwtOptions;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IIdentityApplicationService _identityApplicationService;

		public FacebookAuthController(IOptions<FacebookAuthSettings> fbAuthSettingsAccessor,
			IOptions<JwtIssuerOptions> jwtOptionsAccessor,
			IJwtFactory jwtFactory,
			IHttpClientFactory httpClientFactory,
			IIdentityApplicationService identityApplicationService,
			IClaimsFactory claimsFactory)
		{
			_fbAuthSettings = fbAuthSettingsAccessor.Value;
			_jwtFactory = jwtFactory;
			_jwtOptions = jwtOptionsAccessor.Value;
			_httpClientFactory = httpClientFactory;
			_identityApplicationService = identityApplicationService;
			_claimsFactory = claimsFactory;
		}
		
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] FacebookAuthRequestDto request)
		{
			var appAccessTokenResponse = 
				await _httpClientFactory.GetStringAsync(string.Format(_fbAuthSettings.AccessTokenEndpoint, _fbAuthSettings.AppId, _fbAuthSettings.AppSecret));
			var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

			var userAccessTokenValidationResponse = 
				await _httpClientFactory.GetStringAsync(string.Format(_fbAuthSettings.AccessTokenValidationEndpoint, request.AccessToken, appAccessToken.AccessToken));
			var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

			if (!userAccessTokenValidation.Data.IsValid)
			{
				throw new BadRequestException("Invalid facebook token.");
			}

			var identity = _identityApplicationService.Get(new GetIdentityAdto
			{
				AuthenticationId = userAccessTokenValidation.Data.UserId.ToString()
			});

			var jwt = await Tokens.GenerateJwt(_claimsFactory.GenerateClaimsIdentity(identity.Id, request.AccessToken), _jwtFactory, _jwtOptions);

			return new ObjectResult(jwt);
		}
	}
}