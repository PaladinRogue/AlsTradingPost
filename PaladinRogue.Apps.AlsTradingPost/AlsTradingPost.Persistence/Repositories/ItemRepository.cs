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
    public class ItemRepository : IItemRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public ItemRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        private IQueryable<Item> _filter(Predicate<Item> predicate)
        {
            IQueryable<Item> results = _context.Items.AsNoTracking();

            if (predicate != null)
            {
                results = results.Where(i => predicate(i));
            }

            return results;
        }

        private static IOrderedQueryable<Item> _orderBy<TOrderByKey>(IQueryable<Item> results,
            Expression<Func<Item, TOrderByKey>> orderBy, bool orderByAscending)
        {
            if (orderBy != null)
            {
                results = orderByAscending ? results.OrderBy(orderBy) : results.OrderByDescending(orderBy);
            }

            return (IOrderedQueryable<Item>)results;
        }

        private static IEnumerable<Item> _thenBy<TThenByKey>(IOrderedQueryable<Item> results,
            Expression<Func<Item, TThenByKey>> thenBy, bool thenByAscending)
        {
            if (thenBy != null)
            {
                results = thenByAscending ? results.ThenBy(thenBy) : results.ThenByDescending(thenBy);
            }

            return results;
        }

        public IEnumerable<Item> Get(Predicate<Item> predicate = null)
        {
            return _filter(predicate);
        }

        public IEnumerable<Item> Get<TOrderByKey>(Predicate<Item> predicate = null, Expression<Func<Item, TOrderByKey>> orderBy = null, bool orderByAscending = true)
        {
            return _orderBy(_filter(predicate), orderBy, orderByAscending);
        }

        public IEnumerable<Item> Get<TOrderByKey, TThenByKey>(Predicate<Item> predicate = null,
            Expression<Func<Item, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<Item, TThenByKey>> thenBy = null,
            bool thenByAscending = true)
        {
            return _thenBy(_orderBy(_filter(predicate), orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Item> GetPage(int pageSize, int pageOffset, out int totalResults, Predicate<Item> predicate = null)
        {
            IEnumerable<Item> results = Get(predicate).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Item> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Predicate<Item> predicate = null,
            Expression<Func<Item, TOrderByKey>> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<Item> results = Get(predicate, orderBy, orderByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<Item> GetPage<TOrderByKey, TThenByKey>(int pageSize,
            int pageOffset, out int totalResults,
            Predicate<Item> predicate = null,
            Expression<Func<Item, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<Item, TThenByKey>> thenBy = null,
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