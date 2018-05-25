using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.TraderDomain.Interfaces;
using Common.Resources.Concurrency.Interfaces;

namespace AlsTradingPost.Domain.TraderDomain
{
    public class TraderQueryService : ITraderQueryService
    {
        private readonly ITraderRepository _traderRepository;

        public TraderQueryService(
            ITraderRepository traderRepository)
        {
            _traderRepository = traderRepository;
        }

        public Trader GetById(Guid id)
        {
            return _traderRepository.GetById(id);
        }

        public bool CheckConcurrency(Guid id, IConcurrencyVersion version)
        {
            return _traderRepository.CheckConcurrency(id, version);
        }

        public bool CheckExists(Guid id)
        {
            return _traderRepository.CheckExists(id);
        }
    }
}