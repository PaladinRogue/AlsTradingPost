using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Domain.TraderDomain
{
    public class TraderCommandService : ITraderCommandService
    {
        private readonly ILogger<TraderCommandService> _logger;
        private readonly ITraderRepository _traderRepository;
        
        public TraderCommandService(
            ITraderRepository traderRepository,
            ILogger<TraderCommandService> logger)
        {
            _traderRepository = traderRepository;
            _logger = logger;
        }
        
        public Guid Create(Trader entity)
        {
            try
            {
                _traderRepository.Add(entity);
                
                return entity.Id;
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to create trader");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create trader");
                throw new CreateDomainException(entity, e);
            }
        }

        public void Update(Trader entity)
        {
            try
            {
                _traderRepository.Update(entity);
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to update trader");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update trader");
                throw new UpdateDomainException(entity, e);
            }
        }
    }
}