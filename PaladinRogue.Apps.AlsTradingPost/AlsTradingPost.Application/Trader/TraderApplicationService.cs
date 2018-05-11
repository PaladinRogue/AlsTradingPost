using AlsTradingPost.Application.Trader.Interfaces;
using AlsTradingPost.Application.Trader.Models;
using AlsTradingPost.Domain.TraderDomain;
using AutoMapper;
using FluentValidation;
using Common.Application.Validation;

namespace AlsTradingPost.Application.Trader
{
    public class TraderApplicationService : ITraderApplicationService
    {
        private readonly ITraderDomainService _traderDomainService;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterTraderAdto> _createTraderValidator;

        public TraderApplicationService(
            IValidator<RegisterTraderAdto> createTraderValidator,
            ITraderDomainService traderDomainService,
            IMapper mapper)
        {
            _createTraderValidator = createTraderValidator;
            _traderDomainService = traderDomainService;
            _mapper = mapper;
        }

        public RegisteredTraderAdto Register(RegisterTraderAdto registerTraderAdto)
        {
            _createTraderValidator.ValidateAndThrow(registerTraderAdto);
            
            RegisterTraderDdto registerTraderDdto = _mapper.Map<RegisterTraderAdto, RegisterTraderDdto>(registerTraderAdto);
            
            return _mapper.Map<RegisteredTraderProjection, RegisteredTraderAdto>(_traderDomainService.Register(registerTraderDdto));
        }
    }
}

