using System;
using AlsTradingPost.Api.Trader;
using AlsTradingPost.Application.Trader.Interfaces;
using AlsTradingPost.Application.Trader.Models;
using AlsTradingPost.Setup.Infrastructure.Routing;
using AutoMapper;
using Common.Api.Builders.Resource;
using Common.Application.Authorisation;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    public class TradersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISecure<ITraderApplicationService> _secureTraderApplicationService;
        private readonly IResourceBuilder _resourceBuilder;

        public TradersController(IMapper mapper,
            ISecure<ITraderApplicationService> secureTraderApplicationService,
            IResourceBuilder resourceBuilder)
        {
            _secureTraderApplicationService = secureTraderApplicationService;
            _mapper = mapper;
            _resourceBuilder = resourceBuilder;
        }

        [HttpGet("register/resourceTemplate", Name = RouteDictionary.TraderRegisterResourceTemplate)]
        public IActionResult GetResourceTemplate()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new TraderTemplate())
            );
        }

        [HttpPost("register", Name = RouteDictionary.TraderRegister)]
        public IActionResult Post([FromBody] TraderTemplate template)
        {
            TraderResource resource = _mapper.Map<RegisteredTraderAdto, TraderResource>(
                _secureTraderApplicationService.Service.Register(
                    _mapper.Map<TraderTemplate, RegisterTraderAdto>(template ?? new TraderTemplate())
                )
            );

            return new ObjectResult(
                _resourceBuilder.Build(resource)
            );
        }

        [HttpGet("{id}", Name = RouteDictionary.TraderById)]
        public IActionResult GetById(Guid id)
        {
            TraderResource resource = _mapper.Map<TraderAdto, TraderResource>(
                _secureTraderApplicationService.Service.GetById(id)
            );

            return new ObjectResult(
                _resourceBuilder.Build(resource)
            );
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TraderResource traderResource)
        {
            traderResource.Id = id;
            
            TraderResource resource = _mapper.Map<TraderAdto, TraderResource>(
                _secureTraderApplicationService.Service.Update(_mapper.Map<TraderResource, UpdateTraderAdto>(traderResource))
            );

            return new ObjectResult(
                _resourceBuilder.Build(resource)
            );
        }
    }
}
