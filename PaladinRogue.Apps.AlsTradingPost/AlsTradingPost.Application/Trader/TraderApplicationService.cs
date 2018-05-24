using System;
using AlsTradingPost.Application.Trader.Interfaces;
using AlsTradingPost.Application.Trader.Models;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using AlsTradingPost.Domain.TraderDomain.Models;
using AlsTradingPost.Resources.Authorization;
using AutoMapper;
using Common.Application.Exceptions;
using FluentValidation;
using Common.Application.Validation;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Application.Trader
{
    public class TraderApplicationService : ITraderApplicationService
    {
        private readonly ITraderDomainService _traderDomainService;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterTraderAdto> _registerTraderValidator;
        private readonly IValidator<UpdateTraderAdto> _updateTraderValidator;
        private readonly IConcurrencyQueryService<ITraderQueryService> _traderConcurrencyQueryService;
        private readonly ILogger<TraderApplicationService> _logger;
        private readonly ICurrentUserProvider _currentUserProvider;

        public TraderApplicationService(
            IValidator<RegisterTraderAdto> createTraderValidator,
            ITraderDomainService traderDomainService,
            IMapper mapper,
            IValidator<UpdateTraderAdto> updateTraderValidator,
            IConcurrencyQueryService<ITraderQueryService> traderConcurrencyQueryService,
            ILogger<TraderApplicationService> logger,
            ICurrentUserProvider currentUserProvider)
        {
            _registerTraderValidator = createTraderValidator;
            _traderDomainService = traderDomainService;
            _mapper = mapper;
            _updateTraderValidator = updateTraderValidator;
            _traderConcurrencyQueryService = traderConcurrencyQueryService;
            _logger = logger;
            _currentUserProvider = currentUserProvider;
        }

        public RegisteredTraderAdto Register(RegisterTraderAdto registerTraderAdto)
        {
            _registerTraderValidator.ValidateAndThrow(registerTraderAdto);
            
            RegisterTraderDdto registerTraderDdto = _mapper.Map<RegisterTraderAdto, RegisterTraderDdto>(registerTraderAdto);

            registerTraderDdto.UserId = _currentUserProvider.Id;

            return _mapper.Map<RegisteredTraderProjection, RegisteredTraderAdto>(_traderDomainService.Register(registerTraderDdto));
        }

        public TraderAdto GetById(Guid id)
        {
            return _mapper.Map<TraderProjection, TraderAdto>(_traderDomainService.GetById(id));
        }

        public TraderAdto Update(UpdateTraderAdto updateTraderAdto)
        {
            _updateTraderValidator.ValidateAndThrow(updateTraderAdto);

            try
            {
                _traderConcurrencyQueryService.CheckConcurrency(updateTraderAdto.Id, updateTraderAdto.Version);

                UpdateTraderDdto updateTraderDdto = _mapper.Map<UpdateTraderAdto, UpdateTraderDdto>(updateTraderAdto);

                return _mapper.Map<TraderProjection, TraderAdto>(_traderDomainService.Update(updateTraderDdto));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogInformation(e, "Concurrency exception");
                throw new BusinessApplicationException(ExceptionType.Concurrency, e);
            }
        }
    }
}

