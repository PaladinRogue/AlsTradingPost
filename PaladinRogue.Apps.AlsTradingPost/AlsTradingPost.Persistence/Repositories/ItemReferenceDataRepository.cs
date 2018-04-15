using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence.Repositories
{
    public class ItemReferenceDataRepository : IItemReferenceDataRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public ItemReferenceDataRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IQueryable<ItemReferenceData> Get(Predicate<ItemReferenceData> predicate = null)
        {
            return RepositoryHelper.Filter(_context.ItemReferenceData.AsNoTracking(), predicate);
        }

        public IOrderedQueryable<ItemReferenceData> Get<TOrderByKey>(Predicate<ItemReferenceData> predicate = null, Func<ItemReferenceData, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<ItemReferenceData> Get<TOrderByKey, TThenByKey>(Predicate<ItemReferenceData> predicate = null,
            Func<ItemReferenceData, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<ItemReferenceData, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<ItemReferenceData> GetPage(int pageSize, int pageOffset, out int totalResults, Predicate<ItemReferenceData> predicate = null)
        {
            IEnumerable<ItemReferenceData> results = Get(predicate).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<ItemReferenceData> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Predicate<ItemReferenceData> predicate = null,
            Func<ItemReferenceData, TOrderByKey> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<ItemReferenceData> results = Get(predicate, orderBy, orderByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public IEnumerable<ItemReferenceData> GetPage<TOrderByKey, TThenByKey>(int pageSize,
            int pageOffset, out int totalResults,
            Predicate<ItemReferenceData> predicate = null,
            Func<ItemReferenceData, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<ItemReferenceData, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            IEnumerable<ItemReferenceData> results = Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending).ToList();

            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }

        public ItemReferenceData GetById(Guid id)
        {
            try
            {
                return _context.ItemReferenceData.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public ItemReferenceData GetSingle(Predicate<ItemReferenceData> predicate)
        {
            try
            {
                return _context.ItemReferenceData.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(ItemReferenceData entity)
        {
            _context.ItemReferenceData.Add(entity);

            _context.SaveChanges();
        }

        public void Update(ItemReferenceData entity)
        {
            try
            {
                _context.ItemReferenceData.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            ItemReferenceData entity = GetById(id);

            try
            {
                _context.ItemReferenceData.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}