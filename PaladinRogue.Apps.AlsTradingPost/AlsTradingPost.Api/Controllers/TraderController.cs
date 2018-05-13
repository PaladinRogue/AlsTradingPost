using System;
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
        private readonly IResourceBuilder _resourceBuilder;

        public TraderController(IMapper mapper,
            ITraderApplicationService traderApplicationService,
            ITemplateBuilder templateBuilder,
            IResourceBuilder resourceBuilder)
        {
            _traderApplicationService = traderApplicationService;
            _mapper = mapper;
            _templateBuilder = templateBuilder;
            _resourceBuilder = resourceBuilder;
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
                _resourceBuilder.Create(resource)
                    .WithResourceMeta()
                    .Build()
            );
        }

        [HttpGet("{id}", Name = RouteDictionary.TraderGetById)]
        public IActionResult Post(Guid id)
        {
            TraderResource resource = _mapper.Map<TraderAdto, TraderResource>(
                _traderApplicationService.GetById(id));

            return new ObjectResult(
                _resourceBuilder.Create(resource)
                    .WithResourceMeta()
                    .Build()
            );
        }
    }
}
