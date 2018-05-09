using System;
using System.Threading.Tasks;
using Authentication.Api.Authentication;
using Authentication.Api.Authentication.FacebookModels;
using Authentication.Application.Authentication.Interfaces;
using Authentication.Application.Authentication.Models;
using Authentication.Setup.Settings;
using AutoMapper;
using Common.Api.Authentication;
using Common.Api.Authentication.FacebookModels;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Template;
using Common.Api.HttpClient.Interfaces;
using Common.Application.Authentication;
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
		private readonly IAuthenticationApplicationService _authenticationApplicationService;
		private readonly IResourceTemplateBuilder _resourceTemplateBuilder;
		private readonly ITemplateBuilder _templateBuilder;
		private readonly IResourceBuilder _resourceBuilder;
		private readonly IMapper _mapper;

		public AuthenticationController(IOptions<FacebookAuthSettings> fbAuthSettingsAccessor,
			IJwtFactory jwtFactory,
			IHttpClientFactory httpClientFactory,
			IAuthenticationApplicationService authenticationApplicationService,
			IResourceTemplateBuilder resourceTemplateBuilder,
			ITemplateBuilder templateBuilder,
			IResourceBuilder resourceBuilder,
			IMapper mapper)
		{
			_fbAuthSettings = fbAuthSettingsAccessor.Value;
			_httpClientFactory = httpClientFactory;
			_authenticationApplicationService = authenticationApplicationService;
			_resourceTemplateBuilder = resourceTemplateBuilder;
			_templateBuilder = templateBuilder;
			_resourceBuilder = resourceBuilder;
			_mapper = mapper;
		}

		[Route("services", Name = RouteDictionary.AuthenticationServices)]
		public IActionResult GetAuthenticationServices()
		{
			return new ObjectResult(
				_resourceBuilder.Create(new AuthenticationServicesResource())
					.Build()
			);
		}

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

			ExtendedJwtAdto extendedJwt = await _authenticationApplicationService.LoginAsync(
				new LoginAdto
				{
					AuthenticationId = userAccessTokenValidation.Data.UserId.ToString(),
					AccessToken = template.AccessToken
				});

			return new ObjectResult(
				_resourceTemplateBuilder.Create(_mapper.Map<ExtendedJwtAdto, FacebookJwtResource>(extendedJwt), template)
					.WithResourceMeta()
					.WithTemplateMeta()
					.Build()
			);
		}

		[Route("refreshToken/resourceTemplate", Name = RouteDictionary.AuthenticationRefreshTokenTemplate)]
		public IActionResult GetRefreshTokenTemplate()
		{
			return new ObjectResult(
				_templateBuilder.Create<RefreshTokenTemplate>()
					.WithTemplateMeta()
					.Build()
			);
		}

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