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
    public class UserRepository : IUserRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public UserRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        private IQueryable<User> _filter(Predicate<User> predicate)
        {
            IQueryable<User> results = _context.Users.AsNoTracking();

            if (predicate != null)
            {
                results = results.Where(i => predicate(i));
            }

            return results;
        }

        private static IOrderedQueryable<User> _orderBy<TOrderByKey>(IQueryable<User> results,
            Expression<Func<User, TOrderByKey>> orderBy, bool orderByAscending)
        {
            if (orderBy != null)
            {
                results = orderByAscending ? results.OrderBy(orderBy) : results.OrderByDescending(orderBy);
            }

            return (IOrderedQueryable<User>)results;
        }

        private static IEnumerable<User> _thenBy<TThenByKey>(IOrderedQueryable<User> results,
            Expression<Func<User, TThenByKey>> thenBy, bool thenByAscending)
        {
            if (thenBy != null)
            {
                results = thenByAscending ? results.ThenBy(thenBy) : results.ThenByDescending(thenBy);
            }

            return results;
        }

        public IEnumerable<User> Get(Predicate<User> predicate = null)
        {
            return _filter(predicate);
        }

        public IEnumerable<User> Get<TOrderByKey>(Predicate<User> predicate = null, Expression<Func<User, TOrderByKey>> orderBy = null, bool orderByAscending = true)
        {
            return _orderBy(_filter(predicate), orderBy, orderByAscending);
        }

        public IEnumerable<User> Get<TOrderByKey, TThenByKey>(Predicate<User> predicate = null,
            Expression<Func<User, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<User, TThenByKey>> thenBy = null,
            bool thenByAscending = true)
        {
            return _thenBy(_orderBy(_filter(predicate), orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<User> GetPage(int pageSize, int pageOffset, out int totalResults, Predicate<User> predicate = null)
        {
            IEnumerable<User> results = Get(predicate).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<User> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Predicate<User> predicate = null,
            Expression<Func<User, TOrderByKey>> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<User> results = Get(predicate, orderBy, orderByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<User> GetPage<TOrderByKey, TThenByKey>(int pageSize,
            int pageOffset, out int totalResults,
            Predicate<User> predicate = null,
            Expression<Func<User, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<User, TThenByKey>> thenBy = null,
            bool thenByAscending = true)
        {
            IEnumerable<User> results = Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public User GetById(Guid id)
        {
            try
            {
                return _context.Users.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public User GetSingle(Predicate<User> predicate)
        {
            try
            {
                return _context.Users.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);

            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            try
            {
                _context.Users.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            User entity = GetById(id);

            try
            {
                _context.Users.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}