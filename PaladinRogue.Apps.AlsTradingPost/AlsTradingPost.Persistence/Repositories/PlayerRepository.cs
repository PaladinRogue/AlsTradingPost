using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Persistence.Interfaces;

namespace AlsTradingPost.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public PlayerRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IList<Player> Get()
        {
            throw new NotImplementedException();
        }

        public Player GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(Player player)
        {
            _context.Players.Add(player);
        }

        public void Update(Player obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}