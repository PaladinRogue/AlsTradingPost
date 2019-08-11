using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaladinRogue.Authentication.Application.AuthenticationServices;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models;
using PaladinRogue.Authentication.Setup.Infrastructure.Authorisation;
using PaladinRogue.Authentication.Setup.Infrastructure.Routing;
using PaladinRogue.Libray.Core.Api.Builders.Resource;

namespace PaladinRogue.Authentication.Api.AuthenticationService
{
    [AuthenticationControllerRoute("services")]
    [Authorize(Policies.User)]
    public class AuthenticationServiceController : ControllerBase
    {
        private readonly IResourceBuilder _resourceBuilder;

        private readonly IAuthenticationServiceApplicationService _authenticationServiceApplicationService;

        private readonly IMapper _mapper;

        public AuthenticationServiceController(
            IResourceBuilder resourceBuilder,
            IAuthenticationServiceApplicationService authenticationServiceApplicationService,
            IMapper mapper)
        {
            _resourceBuilder = resourceBuilder;
            _authenticationServiceApplicationService = authenticationServiceApplicationService;
            _mapper = mapper;
        }

        [HttpGet("create", Name = RouteDictionary.GetAuthenticationServiceResourceTemplateTypes)]
        public async Task<IActionResult> GetResetPasswordResourceTemplate()
        {
            IEnumerable<AuthenticationServiceTypeAdto> authenticationServiceTypeAdtos = await _authenticationServiceApplicationService.GetAuthenticationServiceTypes();

            return Ok(_resourceBuilder.BuildCollection(new AuthenticationServiceTypesResource
            {
                Results = _mapper.Map<IEnumerable<AuthenticationServiceTypeAdto>, IList<AuthenticationServiceTypeResource>>(authenticationServiceTypeAdtos)
            }));
        }
    }
}