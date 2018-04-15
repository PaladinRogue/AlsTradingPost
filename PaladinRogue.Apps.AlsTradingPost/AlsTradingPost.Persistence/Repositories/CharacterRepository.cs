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

        public IOrderedQueryable<Character> Get<TOrderByKey>(Predicate<Character> predicate = null, Func<Character, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<Character> Get<TOrderByKey, TThenByKey>(Predicate<Character> predicate = null,
            Func<Character, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Character, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Character> GetPage(int pageSize, int pageOffset, out int totalResults, Predicate<Character> predicate = null)
        {
            IEnumerable<Character> results = Get(predicate).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Character> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Predicate<Character> predicate = null,
            Func<Character, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<Character> results = Get(predicate, orderBy, orderByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Character> GetPage<TOrderByKey, TThenByKey>(int pageSize,
            int pageOffset, out int totalResults,
            Predicate<Character> predicate = null,
            Func<Character, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Character, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            IEnumerable<Character> results = Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public Character GetById(Guid id)
        {
            try
            {
                return _context.Characters.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public Character GetSingle(Predicate<Character> predicate)
        {
            try
            {
                return _context.Characters.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(Character entity)
        {
            _context.Characters.Add(entity);

            _context.SaveChanges();
        }

        public void Update(Character entity)
        {
            try
            {
                _context.Characters.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            Character entity = GetById(id);

            try
            {
                _context.Characters.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}