using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.TraderDomain.Models;
using AlsTradingPost.Resources.Authorization;
using AutoMapper;

namespace AlsTradingPost.Domain.TraderDomain
{
    public class TraderDomainService : ITraderDomainService
    {
        private readonly ITraderCommandService _traderCommandService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserProvider _currentUserProvider;

        public TraderDomainService(
            ICurrentUserProvider currentUserProvider,
            ITraderCommandService traderCommandService,
            IMapper mapper)
        {
            _currentUserProvider = currentUserProvider;
            _traderCommandService = traderCommandService;
            _mapper = mapper;
        }

        public RegisteredTraderProjection Register(RegisterTraderDdto registerTraderDdto)
        {
            CreateTraderDdto newTrader = _mapper.Map<RegisterTraderDdto, CreateTraderDdto>(registerTraderDdto);

            newTrader.Id = _currentUserProvider.Id;
            
            return _mapper.Map<TraderProjection, RegisteredTraderProjection>(_traderCommandService.Create(newTrader));
        }
    }
}