using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Repositories;

namespace AlsTradingPost.Persistence.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public CharacterRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IQueryable<Character> Get(Predicate<Character> predicate = null)
        {
            return RepositoryHelper.Filter(_context.Characters.AsNoTracking(), predicate);
        }

        public IOrderedQueryable<Character> Get<TOrderByKey>(
            Predicate<Character> predicate = null,
            Func<Character, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<Character> Get<TOrderByKey, TThenByKey>(
            Predicate<Character> predicate = null,
            Func<Character, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Character, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Character> GetPage(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Character> predicate = null)
        {
            return RepositoryHelper.GetPage(Get(predicate), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<Character> GetPage<TOrderByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Character> predicate = null,
            Func<Character, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<Character> GetPage<TOrderByKey, TThenByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Character> predicate = null,
            Func<Character, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Character, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending), pageSize, pageOffset, out totalResults);
        }

        public Character GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Characters.AsNoTracking(), id);
        }

        public Character GetSingle(Predicate<Character> predicate)
        {
            return RepositoryHelper.GetSingle(_context.Characters.AsNoTracking(), predicate);
        }

        public void Add(Character entity)
        {
            RepositoryHelper.Add(_context.Characters, _context, entity);
        }

        public void Update(Character entity)
        {
            RepositoryHelper.Update(_context.Characters, _context, entity);
        }

        public void Delete(Guid id)
        {
            RepositoryHelper.Delete(_context.Characters, _context, id);
        }
    }
}