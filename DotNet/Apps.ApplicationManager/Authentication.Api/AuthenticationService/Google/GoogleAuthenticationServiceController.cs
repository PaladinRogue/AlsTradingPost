using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaladinRogue.Authentication.Application.AuthenticationServices;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models.Google;
using PaladinRogue.Authentication.Setup.Infrastructure.Authorisation;
using PaladinRogue.Authentication.Setup.Infrastructure.Routing;
using PaladinRogue.Libray.Core.Api.Builders.Resource;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Concurrency;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Google
{
    [AuthenticationControllerRoute("services/google")]
    [Authorize(Policies.User)]
    public class GoogleAuthenticationServiceController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly IGoogleAuthenticationServiceApplicationService _googleAuthenticationServiceApplicationService;

        private readonly IAuthenticationServiceApplicationService _authenticationServiceApplicationService;

        private readonly IConcurrencyVersionProvider _concurrencyVersionProvider;

        private readonly IMapper _mapper;

        public GoogleAuthenticationServiceController(
            IResourceBuilder resourceBuilder,
            IGoogleAuthenticationServiceApplicationService googleAuthenticationServiceApplicationService,
            IAuthenticationServiceApplicationService authenticationServiceApplicationService,
            IConcurrencyVersionProvider concurrencyVersionProvider,
            IMapper mapper)
        {
            _resourceBuilder = resourceBuilder;
            _googleAuthenticationServiceApplicationService = googleAuthenticationServiceApplicationService;
            _authenticationServiceApplicationService = authenticationServiceApplicationService;
            _mapper = mapper;
            _concurrencyVersionProvider = concurrencyVersionProvider;
        }

        [HttpGet("resourceTemplate", Name = RouteDictionary.GoogleAuthenticationServiceResourceTemplate)]
        public IActionResult GetResetPasswordResourceTemplate()
        {
            return Ok(_resourceBuilder.Build(new GoogleAuthenticationServiceTemplate()));
        }

        [HttpPost("", Name = RouteDictionary.CreateGoogleAuthenticationService)]
        public async Task<IActionResult> Post(GoogleAuthenticationServiceTemplate template)
        {
            GoogleAdto googleAdto = await
                _googleAuthenticationServiceApplicationService.CreateAsync(_mapper.Map<GoogleAuthenticationServiceTemplate, CreateGoogleAdto>(template));

            return CreatedAtRoute(RouteDictionary.GetGoogleAuthenticationService, new {googleAdto.Id},
                _resourceBuilder.Build(_mapper.Map<GoogleAdto, GoogleAuthenticationServiceResource>(googleAdto)));
        }

        [HttpGet("{id}", Name = RouteDictionary.GetGoogleAuthenticationService)]
        public async Task<IActionResult> Get(Guid id)
        {
            GoogleAdto googleAdto = await
                _googleAuthenticationServiceApplicationService.GetAsync(new GetGoogleAdto
                {
                    Id = id
                });

            return Ok(_resourceBuilder.Build(_mapper.Map<GoogleAdto, GoogleAuthenticationServiceResource>(googleAdto)));
        }

        [HttpPut("{id}", Name = RouteDictionary.ChangeGoogleAuthenticationService)]
        public async Task<IActionResult> Put(
            Guid id,
            GoogleAuthenticationServiceResource resource)
        {
            ChangeGoogleAdto changeGoogleAdto = new ChangeGoogleAdto
            {
                Id = id,
                Name = resource.Name,
                ClientId = resource.ClientId,
                ClientSecret = resource.ClientSecret,
                GrantAccessTokenUrl = resource.GrantAccessTokenUrl,
                ValidateAccessTokenUrl = resource.ValidateAccessTokenUrl,
                ClientGrantAccessTokenUrl = resource.ClientGrantAccessTokenUrl,
                Version = resource.Version
            };

            GoogleAdto googleAdto = await
                _googleAuthenticationServiceApplicationService.ChangeAsync(changeGoogleAdto);

            return Ok(_resourceBuilder.Build(_mapper.Map<GoogleAdto, GoogleAuthenticationServiceResource>(googleAdto)));
        }

        [HttpDelete("{id}", Name = RouteDictionary.DeleteGoogleAuthenticationService)]
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