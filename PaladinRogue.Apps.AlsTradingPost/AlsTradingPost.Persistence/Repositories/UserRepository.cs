using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
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

        public IOrderedQueryable<User> Get<TOrderByKey>(
            Predicate<User> predicate = null,
            Func<User, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<User> Get<TOrderByKey, TThenByKey>(
            Predicate<User> predicate = null,
            Func<User, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<User, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<User> GetPage(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<User> predicate = null)
        {
            return RepositoryHelper.GetPage(Get(predicate), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<User> GetPage<TOrderByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<User> predicate = null,
            Func<User, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<User> GetPage<TOrderByKey, TThenByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<User> predicate = null,
            Func<User, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<User, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending), pageSize, pageOffset, out totalResults);
        }

        public User GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Users.AsNoTracking(), id);
        }

        public User GetSingle(Predicate<User> predicate)
        {
            return RepositoryHelper.GetSingle(_context.Users.AsNoTracking(), predicate);
        }

        public void Add(User entity)
        {
            RepositoryHelper.Add(_context.Users, _context, entity);
        }

        public void Update(User entity)
        {
            RepositoryHelper.Update(_context.Users, _context, entity);
        }

        public void Delete(Guid id)
        {
            RepositoryHelper.Delete(_context.Users, _context, id);
        }
    }
}