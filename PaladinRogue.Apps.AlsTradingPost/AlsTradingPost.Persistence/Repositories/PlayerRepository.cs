using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Player> Get(Predicate<Player> predicate = null)
        {
            return RepositoryHelper.Filter(_context.Players.AsNoTracking(), predicate);
        }

        public IOrderedQueryable<Player> Get<TOrderByKey>(Predicate<Player> predicate = null, Func<Player, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<Player> Get<TOrderByKey, TThenByKey>(Predicate<Player> predicate = null,
            Func<Player, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Player, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Player> GetPage(int pageSize, int pageOffset, out int totalResults, Predicate<Player> predicate = null)
        {
            IEnumerable<Player> results = Get(predicate).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Player> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Predicate<Player> predicate = null,
            Func<Player, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<Player> results = Get(predicate, orderBy, orderByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Player> GetPage<TOrderByKey, TThenByKey>(int pageSize,
            int pageOffset, out int totalResults,
            Predicate<Player> predicate = null,
            Func<Player, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Player, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            IEnumerable<Player> results = Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
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