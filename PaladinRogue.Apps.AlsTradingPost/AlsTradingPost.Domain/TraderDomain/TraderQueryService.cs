using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.TraderDomain.Events;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.TraderDomain.Models;
using AutoMapper;
using Common.Domain.DomainEvents;

namespace AlsTradingPost.Domain.TraderDomain
{
    public class TraderQueryService : ITraderQueryService
    {
        private readonly ITraderRepository _traderRepository;
        private readonly IMapper _mapper;

        public TraderQueryService(
            ITraderRepository traderRepository,
            IMapper mapper)
        {
            _traderRepository = traderRepository;
            _mapper = mapper;
        }

        public TraderProjection GetById(Guid id)
        {
            Trader trader = _traderRepository.GetById(id);
            
            DomainEvents.Raise(TraderReadDomainEvent.Create(trader));
            
            return _mapper.Map<Trader, TraderProjection>(trader);
        }
    }
}