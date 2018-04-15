using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

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

        public IOrderedQueryable<Admin> Get<TOrderByKey>(Predicate<Admin> predicate = null, Func<Admin, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<Admin> Get<TOrderByKey, TThenByKey>(Predicate<Admin> predicate = null,
            Func<Admin, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Admin, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Admin> GetPage(int pageSize, int pageOffset, out int totalResults, Predicate<Admin> predicate = null)
        {
            IEnumerable<Admin> results = Get(predicate).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Admin> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Predicate<Admin> predicate = null,
            Func<Admin, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<Admin> results = Get(predicate, orderBy, orderByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Admin> GetPage<TOrderByKey, TThenByKey>(int pageSize,
            int pageOffset, out int totalResults,
            Predicate<Admin> predicate = null,
            Func<Admin, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Admin, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            IEnumerable<Admin> results = Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public Admin GetById(Guid id)
        {
            try
            {
                return _context.Admins.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public Admin GetSingle(Predicate<Admin> predicate)
        {
            try
            {
                return _context.Admins.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(Admin entity)
        {
            _context.Admins.Add(entity);

            _context.SaveChanges();
        }

        public void Update(Admin entity)
        {
            try
            {
                _context.Admins.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            Admin entity = GetById(id);

            try
            {
                _context.Admins.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}