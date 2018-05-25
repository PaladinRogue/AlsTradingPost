using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Resources.Concurrency.Interfaces;
using Persistence.EntityFramework.Repositories;

namespace AlsTradingPost.Persistence.Repositories
{
    public class TraderRepository : ITraderRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public TraderRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public Trader GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Traders, id);
        }

        public void Add(Trader entity)
        {
            RepositoryHelper.Add(_context.Traders, _context, entity);
        }

        public void Update(Trader entity)
        {
            RepositoryHelper.Update(_context.Traders, _context, entity);
        }

        public bool CheckConcurrency(Guid id, IConcurrencyVersion version)
        {
            return RepositoryHelper.CheckConcurrency(_context.Traders, id, version.Version);
        }

        public bool CheckExists(Guid id)
        {
            return RepositoryHelper.CheckExists(_context.Traders, id);
        }
    }
}