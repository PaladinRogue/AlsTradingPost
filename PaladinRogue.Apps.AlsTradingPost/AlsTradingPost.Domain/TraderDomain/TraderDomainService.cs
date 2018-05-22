using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.TraderDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.TraderDomain
{
    public class TraderDomainService : ITraderDomainService
    {
        private readonly ITraderCommandService _traderCommandService;
        private readonly IMapper _mapper;

        public TraderDomainService(
            ITraderCommandService traderCommandService,
            IMapper mapper)
        {
            _traderCommandService = traderCommandService;
            _mapper = mapper;
        }

        public RegisteredTraderProjection Register(RegisterTraderDdto registerTraderDdto)
        {
            CreateTraderDdto newTrader = _mapper.Map<RegisterTraderDdto, CreateTraderDdto>(registerTraderDdto);

            //TODO: Validate user exists
            
            return _mapper.Map<TraderProjection, RegisteredTraderProjection>(_traderCommandService.Create(newTrader));
        }
    }
}