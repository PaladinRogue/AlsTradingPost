using System;
using System.Threading.Tasks;
using Authentication.ApplicationServices.AuthenticationServices;
using Authentication.ApplicationServices.AuthenticationServices.Models;
using Authentication.ApplicationServices.AuthenticationServices.Models.Facebook;
using Authentication.Setup.Infrastructure.Authorisation;
using Authentication.Setup.Infrastructure.Routing;
using AutoMapper;
using Common.Api.Builders.Resource;
using Common.Setup.Infrastructure.Concurrency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.AuthenticationService.Facebook
{
    [AuthenticationControllerRoute("services/facebook")]
    [Authorize(Policies.User)]
    public class FacebookAuthenticationServiceController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly IFacebookAuthenticationServiceApplicationService _facebookAuthenticationServiceApplicationService;

        private readonly IAuthenticationServiceApplicationService _authenticationServiceApplicationService;

        private readonly IConcurrencyVersionProvider _concurrencyVersionProvider;

        private readonly IMapper _mapper;

        public FacebookAuthenticationServiceController(
            IResourceBuilder resourceBuilder,
            IFacebookAuthenticationServiceApplicationService facebookAuthenticationServiceApplicationService,
            IAuthenticationServiceApplicationService authenticationServiceApplicationService,
            IConcurrencyVersionProvider concurrencyVersionProvider,
            IMapper mapper)
        {
            _resourceBuilder = resourceBuilder;
            _facebookAuthenticationServiceApplicationService = facebookAuthenticationServiceApplicationService;
            _authenticationServiceApplicationService = authenticationServiceApplicationService;
            _mapper = mapper;
            _concurrencyVersionProvider = concurrencyVersionProvider;
        }

        [HttpGet("resourceTemplate", Name = RouteDictionary.FacebookAuthenticationServiceResourceTemplate)]
        public IActionResult GetResetPasswordResourceTemplate()
        {
            return Ok(_resourceBuilder.Build(new FacebookAuthenticationServiceTemplate()));
        }

        [HttpPost("", Name = RouteDictionary.CreateFacebookAuthenticationService)]
        public async Task<IActionResult> Post(FacebookAuthenticationServiceTemplate template)
        {
            FacebookAdto facebookAdto = await
                _facebookAuthenticationServiceApplicationService.CreateAsync(_mapper.Map<FacebookAuthenticationServiceTemplate, CreateFacebookAdto>(template));

            return CreatedAtRoute(RouteDictionary.GetFacebookAuthenticationService, new {facebookAdto.Id},
                _resourceBuilder.Build(_mapper.Map<FacebookAdto, FacebookAuthenticationServiceResource>(facebookAdto)));
        }

        [HttpGet("{id}", Name = RouteDictionary.GetFacebookAuthenticationService)]
        public async Task<IActionResult> Get(Guid id)
        {
            FacebookAdto facebookAdto = await
                _facebookAuthenticationServiceApplicationService.GetAsync(new GetFacebookAdto
                {
                    Id = id
                });

            return Ok(_resourceBuilder.Build(_mapper.Map<FacebookAdto, FacebookAuthenticationServiceResource>(facebookAdto)));
        }

        [HttpPut("{id}", Name = RouteDictionary.ChangeFacebookAuthenticationService)]
        public async Task<IActionResult> Put(
            Guid id,
            FacebookAuthenticationServiceResource resource)
        {
            ChangeFacebookAdto changeFacebookAdto = new ChangeFacebookAdto
            {
                Id = id,
                Name = resource.Name,
                ClientId = resource.ClientId,
                ClientSecret = resource.ClientSecret,
                GrantAccessTokenUrl = resource.GrantAccessTokenUrl,
                ValidateAccessTokenUrl = resource.ValidateAccessTokenUrl,
                ClientGrantAccessTokenUrl = resource.ClientGrantAccessTokenUrl,
                AppAccessToken = resource.AppAccessToken,
                Version = resource.Version
            };

            FacebookAdto facebookAdto = await
                _facebookAuthenticationServiceApplicationService.ChangeAsync(changeFacebookAdto);

            return Ok(_resourceBuilder.Build(_mapper.Map<FacebookAdto, FacebookAuthenticationServiceResource>(facebookAdto)));
        }

        [HttpDelete("{id}", Name = RouteDictionary.DeleteFacebookAuthenticationService)]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteClientCredentialAdto deleteClientCredentialAdto = new DeleteClientCredentialAdto
            {
                Id = id,
                Version = _concurrencyVersionProvider.Get()
            };

            await
                _authenticationServiceApplicationService.DeleteClientCredentialAsync(deleteClientCredentialAdto);

            return NoContent();
        }
    }
}