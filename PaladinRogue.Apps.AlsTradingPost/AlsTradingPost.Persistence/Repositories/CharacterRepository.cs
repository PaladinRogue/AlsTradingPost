using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public CharacterRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        private IQueryable<Character> _filter(Predicate<Character> predicate)
        {
            IQueryable<Character> results = _context.Characters.AsNoTracking();

            if (predicate != null)
            {
                results = results.Where(i => predicate(i));
            }

            return results;
        }

        private static IOrderedQueryable<Character> _orderBy<TOrderByKey>(IQueryable<Character> results,
            Expression<Func<Character, TOrderByKey>> orderBy, bool orderByAscending)
        {
            if (orderBy != null)
            {
                results = orderByAscending ? results.OrderBy(orderBy) : results.OrderByDescending(orderBy);
            }

            return (IOrderedQueryable<Character>)results;
        }

        private static IEnumerable<Character> _thenBy<TThenByKey>(IOrderedQueryable<Character> results,
            Expression<Func<Character, TThenByKey>> thenBy, bool thenByAscending)
        {
            if (thenBy != null)
            {
                results = thenByAscending ? results.ThenBy(thenBy) : results.ThenByDescending(thenBy);
            }

            return results;
        }

        public IEnumerable<Character> Get(Predicate<Character> predicate = null)
        {
            return _filter(predicate);
        }

        public IEnumerable<Character> Get<TOrderByKey>(Predicate<Character> predicate = null, Expression<Func<Character, TOrderByKey>> orderBy = null, bool orderByAscending = true)
        {
            return _orderBy(_filter(predicate), orderBy, orderByAscending);
        }

        public IEnumerable<Character> Get<TOrderByKey, TThenByKey>(Predicate<Character> predicate = null,
            Expression<Func<Character, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<Character, TThenByKey>> thenBy = null,
            bool thenByAscending = true)
        {
            return _thenBy(_orderBy(_filter(predicate), orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Character> GetPage(int pageSize, int pageOffset, out int totalResults, Predicate<Character> predicate = null)
        {
            IEnumerable<Character> results = Get(predicate).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Character> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Predicate<Character> predicate = null,
            Expression<Func<Character, TOrderByKey>> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<Character> results = Get(predicate, orderBy, orderByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Character> GetPage<TOrderByKey, TThenByKey>(int pageSize,
            int pageOffset, out int totalResults,
            Predicate<Character> predicate = null,
            Expression<Func<Character, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<Character, TThenByKey>> thenBy = null,
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