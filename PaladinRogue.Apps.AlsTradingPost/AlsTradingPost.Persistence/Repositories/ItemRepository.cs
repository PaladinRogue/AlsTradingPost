using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public ItemRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IQueryable<Item> Get(Predicate<Item> predicate = null)
        {
            return RepositoryHelper.Filter(_context.Items.AsNoTracking(), predicate);
        }

        public IOrderedQueryable<Item> Get<TOrderByKey>(Predicate<Item> predicate = null, Func<Item, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<Item> Get<TOrderByKey, TThenByKey>(Predicate<Item> predicate = null,
            Func<Item, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Item, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Item> GetPage(int pageSize, int pageOffset, out int totalResults, Predicate<Item> predicate = null)
        {
            IEnumerable<Item> results = Get(predicate).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Item> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Predicate<Item> predicate = null,
            Func<Item, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<Item> results = Get(predicate, orderBy, orderByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Item> GetPage<TOrderByKey, TThenByKey>(int pageSize,
            int pageOffset, out int totalResults,
            Predicate<Item> predicate = null,
            Func<Item, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Item, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            IEnumerable<Item> results = Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public Item GetById(Guid id)
        {
            try
            {
                return _context.Items.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public Item GetSingle(Predicate<Item> predicate)
        {
            try
            {
                return _context.Items.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(Item entity)
        {
            _context.Items.Add(entity);

            _context.SaveChanges();
        }

        public void Update(Item entity)
        {
            try
            {
                _context.Items.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            Item entity = GetById(id);

            try
            {
                _context.Items.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}