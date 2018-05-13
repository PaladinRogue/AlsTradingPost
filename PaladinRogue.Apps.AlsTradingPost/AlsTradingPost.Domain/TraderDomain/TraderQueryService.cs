using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.TraderDomain.Models;
using AutoMapper;

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
           return _mapper.Map<Trader, TraderProjection>(_traderRepository.GetById(id));
        }
    }
}