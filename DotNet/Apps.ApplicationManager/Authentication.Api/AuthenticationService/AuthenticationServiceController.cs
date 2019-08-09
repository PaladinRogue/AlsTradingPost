using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Application.AuthenticationServices;
using Authentication.Application.AuthenticationServices.Models;
using Authentication.Setup.Infrastructure.Authorisation;
using Authentication.Setup.Infrastructure.Routing;
using AutoMapper;
using Common.Api.Builders.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.AuthenticationService
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