using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Persistence.EntityFramework.Repositories;

namespace AlsTradingPost.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public PlayerRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public Player GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Players, id);
        }
    }
}