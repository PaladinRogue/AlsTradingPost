using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.TraderDomain.Models;
using AutoMapper;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Domain.TraderDomain
{
    public class TraderCommandService : ITraderCommandService
    {
        private readonly ILogger<TraderCommandService> _logger;
        private readonly ITraderRepository _traderRepository;
        private readonly IMapper _mapper;
        
        public TraderCommandService(
            ITraderRepository traderRepository,
            IMapper mapper,
            ILogger<TraderCommandService> logger)
        {
            _traderRepository = traderRepository;
            _mapper = mapper;
            _logger = logger;
        }
        
        public TraderProjection Create(CreateTraderDdto entity)
        {
            Trader trader = null;
            try
            {
                trader = _mapper.Map(entity, EntityFactory.CreateEntity<Trader>(entity.Id));

                _traderRepository.Add(trader);

                return _mapper.Map<Trader, TraderProjection>(_traderRepository.GetById(trader.Id));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to create trader");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create trader");
                throw new CreateDomainException(trader, e);
            }
        }
    }
}