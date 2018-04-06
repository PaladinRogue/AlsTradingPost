using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public PlayerRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Player> Get()
        {
            return _context.Players.AsNoTracking();
        }

        public Player GetById(Guid id)
        {
            try
            {
                return _context.Players.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public Player GetSingle(Predicate<Player> predicate)
        {
            try
            {
                return _context.Players.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(Player entity)
        {
            _context.Players.Add(entity);

            _context.SaveChanges();
        }

        public void Update(Player entity)
        {
            try
            {
                _context.Players.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            Player entity = GetById(id);

            try
            {
                _context.Players.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}