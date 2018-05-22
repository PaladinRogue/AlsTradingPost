using AlsTradingPost.Domain.TraderDomain.Exceptions;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.TraderDomain.Models;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AutoMapper;

namespace AlsTradingPost.Domain.TraderDomain
{
    public class TraderDomainService : ITraderDomainService
    {
        private readonly ITraderCommandService _traderCommandService;
        private readonly IUserQueryService _userQueryService;
        private readonly IMapper _mapper;

        public TraderDomainService(
            ITraderCommandService traderCommandService,
            IMapper mapper,
            IUserQueryService userQueryService)
        {
            _traderCommandService = traderCommandService;
            _mapper = mapper;
            _userQueryService = userQueryService;
        }

        public RegisteredTraderProjection Register(RegisterTraderDdto registerTraderDdto)
        {
            if (_userQueryService.CheckExists(registerTraderDdto.UserId))
            {
                throw new UserDoesNotExistDomainException();
            }

            CreateTraderDdto newTrader = _mapper.Map<RegisterTraderDdto, CreateTraderDdto>(registerTraderDdto);
            
            return _mapper.Map<TraderProjection, RegisteredTraderProjection>(_traderCommandService.Create(newTrader));
        }
    }
}