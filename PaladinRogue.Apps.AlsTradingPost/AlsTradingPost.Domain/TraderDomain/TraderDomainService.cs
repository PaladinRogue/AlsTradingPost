using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.TraderDomain.Events;
using AlsTradingPost.Domain.TraderDomain.Exceptions;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.TraderDomain.Models;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AutoMapper;
using Common.Domain.DomainEvents;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.TraderDomain
{
    public class TraderDomainService : ITraderDomainService
    {
        private readonly ITraderCommandService _traderCommandService;
        private readonly ITraderQueryService _traderQueryService;
        private readonly IUserQueryService _userQueryService;
        private readonly IMapper _mapper;

        public TraderDomainService(
            ITraderCommandService traderCommandService,
            IMapper mapper,
            IUserQueryService userQueryService,
            ITraderQueryService traderQueryService)
        {
            _traderCommandService = traderCommandService;
            _mapper = mapper;
            _userQueryService = userQueryService;
            _traderQueryService = traderQueryService;
        }

        public RegisteredTraderProjection Register(RegisterTraderDdto registerTraderDdto)
        {
            if (!_userQueryService.CheckExists(registerTraderDdto.UserId))
            {
                throw new UserDoesNotExistDomainException();
            }

            if (_traderQueryService.CheckExists(registerTraderDdto.UserId))
            {
                throw new TraderAlreadyExistsDomainException();
            }
            
            Trader trader = _mapper.Map(registerTraderDdto, AggregateFactory.CreateRoot<Trader>(registerTraderDdto.UserId));

            Guid traderId = _traderCommandService.Create(trader);

            Trader newTrader = _traderQueryService.GetById(traderId);

            DomainEvents.Raise(TraderCreatedDomainEvent.Create(newTrader));

            return _mapper.Map<Trader, RegisteredTraderProjection>(newTrader);
        }

        public TraderProjection Update(UpdateTraderDdto updateTraderDdto)
        {
            Trader trader = _mapper.Map<UpdateTraderDdto, Trader>(updateTraderDdto);

            _traderCommandService.Update(trader);

            Trader updatedTrader = _traderQueryService.GetById(trader.Id);

            DomainEvents.Raise(TraderUpdatedDomainEvent.Create(updatedTrader));

            return _mapper.Map<Trader, TraderProjection>(updatedTrader);
        }

        public TraderProjection GetById(Guid id)
        {
            Trader trader = _traderQueryService.GetById(id);

            DomainEvents.Raise(TraderReadDomainEvent.Create(trader));

            return _mapper.Map<Trader, TraderProjection>(trader);
        }
    }
}