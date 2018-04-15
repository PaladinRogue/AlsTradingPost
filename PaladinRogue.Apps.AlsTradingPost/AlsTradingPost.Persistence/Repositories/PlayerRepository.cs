using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
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

        public IOrderedQueryable<Player> Get<TOrderByKey>(
            Predicate<Player> predicate = null,
            Func<Player, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<Player> Get<TOrderByKey, TThenByKey>(
            Predicate<Player> predicate = null,
            Func<Player, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Player, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Player> GetPage(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Player> predicate = null)
        {
            return RepositoryHelper.GetPage(Get(predicate), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<Player> GetPage<TOrderByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Player> predicate = null,
            Func<Player, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<Player> GetPage<TOrderByKey, TThenByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Player> predicate = null,
            Func<Player, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Player, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending), pageSize, pageOffset, out totalResults);
        }

        public Player GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Players.AsNoTracking(), id);
        }

        public Player GetSingle(Predicate<Player> predicate)
        {
            return RepositoryHelper.GetSingle(_context.Players.AsNoTracking(), predicate);
        }

        public void Add(Player entity)
        {
            RepositoryHelper.Add(_context.Players, _context, entity);
        }

        public void Update(Player entity)
        {
            RepositoryHelper.Update(_context.Players, _context, entity);
        }

        public void Delete(Guid id)
        {
            RepositoryHelper.Delete(_context.Players, _context, id);
        }
    }
}