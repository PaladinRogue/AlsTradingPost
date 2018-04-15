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
    public class UserRepository : IUserRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public UserRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> Get(Predicate<User> predicate = null)
        {
            return RepositoryHelper.Filter(_context.Users.AsNoTracking(), predicate);
        }

        public IOrderedQueryable<User> Get<TOrderByKey>(Predicate<User> predicate = null, Func<User, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<User> Get<TOrderByKey, TThenByKey>(Predicate<User> predicate = null,
            Func<User, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<User, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<User> GetPage(int pageSize, int pageOffset, out int totalResults, Predicate<User> predicate = null)
        {
            IEnumerable<User> results = Get(predicate).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<User> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Predicate<User> predicate = null,
            Func<User, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<User> results = Get(predicate, orderBy, orderByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<User> GetPage<TOrderByKey, TThenByKey>(int pageSize,
            int pageOffset, out int totalResults,
            Predicate<User> predicate = null,
            Func<User, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<User, TThenByKey> thenBy = null,
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