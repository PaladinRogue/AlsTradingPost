using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaladinRogue.Gateway.Application.Applications;
using PaladinRogue.Libray.Core.Api.Builders.Resource;

namespace PaladinRogue.Gateway.Api.Aggregation
{
    [Route("")]
    public class AggregationController : ControllerBase
    {
        private readonly IApplicationApplicationService _applicationApplicationService;

        private readonly IResourceBuilder _resourceBuilder;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AggregationController(
            IApplicationApplicationService applicationApplicationService,
            IResourceBuilder resourceBuilder,
            IHttpContextAccessor httpContextAccessor)
        {
            _applicationApplicationService = applicationApplicationService;
            _resourceBuilder = resourceBuilder;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpGet("", Name = RouteDictionary.Aggregation)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<ApplicationResource> applicationResources = (await _applicationApplicationService.GetAllAsync()).Select(a => new ApplicationResource
            {
                Name = a.Name,
                SystemName = a.SystemName
            });

            return Ok(_resourceBuilder.BuildCollection(new ApplicationsResource
            {
                Results = applicationResources.ToList()
            }));
        }
    }
}