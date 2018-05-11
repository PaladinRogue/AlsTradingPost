using AlsTradingPost.Api.Trader;
using AlsTradingPost.Application.Trader.Interfaces;
using AlsTradingPost.Application.Trader.Models;
using AutoMapper;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Template;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    public class TraderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITraderApplicationService _traderApplicationService;
        private readonly ITemplateBuilder _templateBuilder;
        private readonly IResourceTemplateBuilder _resourceTemplateBuilder;

        public TraderController(IMapper mapper,
            ITraderApplicationService traderApplicationService,
            ITemplateBuilder templateBuilder,
            IResourceTemplateBuilder resourceTemplateBuilder)
        {
            _traderApplicationService = traderApplicationService;
            _mapper = mapper;
            _templateBuilder = templateBuilder;
            _resourceTemplateBuilder = resourceTemplateBuilder;
        }

        [HttpGet("register/resourceTemplate", Name = RouteDictionary.TraderRegisterResourceTemplate)]
        public IActionResult GetResourceTemplate()
        {
            return new ObjectResult(
                _templateBuilder.Create<TraderTemplate>()
                    .WithTemplateMeta()
                    .Build()
            );
        }

        [HttpPost("register", Name = RouteDictionary.TraderRegister)]
        public IActionResult Post([FromBody] TraderTemplate template)
        {
            TraderResource resource = _mapper.Map<RegisteredTraderAdto, TraderResource>(
                _traderApplicationService.Register(_mapper.Map<TraderTemplate, RegisterTraderAdto>(template ?? new TraderTemplate())));

            return new ObjectResult(
                _resourceTemplateBuilder.Create(resource, template)
                    .WithResourceMeta()
                    .WithTemplateMeta()
                    .Build()
            );
        }
    }
}
