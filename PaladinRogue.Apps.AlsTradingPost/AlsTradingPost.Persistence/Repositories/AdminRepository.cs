using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Repositories;

namespace AlsTradingPost.Persistence.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public AdminRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IQueryable<Admin> Get(Predicate<Admin> predicate = null)
        {
            return RepositoryHelper.Filter(_context.Admins.AsNoTracking(), predicate);
        }

        public IOrderedQueryable<Admin> Get<TOrderByKey>(
            Predicate<Admin> predicate = null,
            Func<Admin, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<Admin> Get<TOrderByKey, TThenByKey>(
            Predicate<Admin> predicate = null,
            Func<Admin, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Admin, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Admin> GetPage(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Admin> predicate = null)
        {
            return RepositoryHelper.GetPage(Get(predicate), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<Admin> GetPage<TOrderByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Admin> predicate = null,
            Func<Admin, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<Admin> GetPage<TOrderByKey, TThenByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Admin> predicate = null,
            Func<Admin, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Admin, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending), pageSize, pageOffset, out totalResults);
        }

        public Admin GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Admins.AsNoTracking(), id);
        }

        public Admin GetSingle(Predicate<Admin> predicate)
        {
            return RepositoryHelper.GetSingle(_context.Admins.AsNoTracking(), predicate);
        }

        public void Add(Admin entity)
        {
            RepositoryHelper.Add(_context.Admins, _context, entity);
        }

        public void Update(Admin entity)
        {
            RepositoryHelper.Update(_context.Admins, _context, entity);
        }

        public void Delete(Guid id)
        {
            RepositoryHelper.Delete(_context.Admins, _context, id);
        }
    }
}