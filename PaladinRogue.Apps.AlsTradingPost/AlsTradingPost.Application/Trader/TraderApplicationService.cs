using System;
using AlsTradingPost.Application.Trader.Interfaces;
using AlsTradingPost.Application.Trader.Models;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.TraderDomain.Models;
using AutoMapper;
using Common.Application.Exceptions;
using FluentValidation;
using Common.Application.Validation;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using ApplicationException = Common.Application.Exceptions.ApplicationException;

namespace AlsTradingPost.Application.Trader
{
    public class TraderApplicationService : ITraderApplicationService
    {
        private readonly ITraderDomainService _traderDomainService;
        private readonly ITraderCommandService _traderCommandService;
        private readonly ITraderQueryService _traderQueryService;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterTraderAdto> _registerTraderValidator;
        private readonly IValidator<UpdateTraderAdto> _updateTraderValidator;
        private readonly IConcurrencyQueryService<ITraderQueryService> _concurrencyQueryService;
        private readonly ILogger<TraderApplicationService> _logger;

        public TraderApplicationService(
            IValidator<RegisterTraderAdto> createTraderValidator,
            ITraderDomainService traderDomainService,
            IMapper mapper,
            ITraderQueryService traderQueryService,
            IValidator<UpdateTraderAdto> updateTraderValidator,
            ITraderCommandService traderCommandService,
            IConcurrencyQueryService<ITraderQueryService> concurrencyQueryService,
            ILogger<TraderApplicationService> logger)
        {
            _registerTraderValidator = createTraderValidator;
            _traderDomainService = traderDomainService;
            _mapper = mapper;
            _traderQueryService = traderQueryService;
            _updateTraderValidator = updateTraderValidator;
            _traderCommandService = traderCommandService;
            _concurrencyQueryService = concurrencyQueryService;
            _logger = logger;
        }

        public RegisteredTraderAdto Register(RegisterTraderAdto registerTraderAdto)
        {
            _registerTraderValidator.ValidateAndThrow(registerTraderAdto);
            
            RegisterTraderDdto registerTraderDdto = _mapper.Map<RegisterTraderAdto, RegisterTraderDdto>(registerTraderAdto);
            
            return _mapper.Map<RegisteredTraderProjection, RegisteredTraderAdto>(_traderDomainService.Register(registerTraderDdto));
        }

        public TraderAdto GetById(Guid id)
        {
            return _mapper.Map<TraderProjection, TraderAdto>(_traderQueryService.GetById(id));
        }

        public TraderAdto Update(UpdateTraderAdto updateTraderAdto)
        {
            _updateTraderValidator.ValidateAndThrow(updateTraderAdto);

            try
            {
                _concurrencyQueryService.CheckConcurrency(updateTraderAdto.Id, updateTraderAdto.Version);

                UpdateTraderDdto updateTraderDdto = _mapper.Map<UpdateTraderAdto, UpdateTraderDdto>(updateTraderAdto);

                return _mapper.Map<TraderProjection, TraderAdto>(_traderCommandService.Update(updateTraderDdto));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogInformation(e, "Concurrency exception");
                throw new ApplicationException(ExceptionType.Concurrency, e);
            }
        }
    }
}

