using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Repositories;

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

        public IOrderedQueryable<ItemReferenceData> Get<TOrderByKey>(
            Predicate<ItemReferenceData> predicate = null,
            Func<ItemReferenceData, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<ItemReferenceData> Get<TOrderByKey, TThenByKey>(
            Predicate<ItemReferenceData> predicate = null,
            Func<ItemReferenceData, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<ItemReferenceData, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<ItemReferenceData> GetPage(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<ItemReferenceData> predicate = null)
        {
            return RepositoryHelper.GetPage(Get(predicate), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<ItemReferenceData> GetPage<TOrderByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<ItemReferenceData> predicate = null,
            Func<ItemReferenceData, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<ItemReferenceData> GetPage<TOrderByKey, TThenByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<ItemReferenceData> predicate = null,
            Func<ItemReferenceData, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<ItemReferenceData, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending), pageSize, pageOffset, out totalResults);
        }

        public ItemReferenceData GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.ItemReferenceData.AsNoTracking(), id);
        }

        public ItemReferenceData GetSingle(Predicate<ItemReferenceData> predicate)
        {
            return RepositoryHelper.GetSingle(_context.ItemReferenceData.AsNoTracking(), predicate);
        }

        public void Add(ItemReferenceData entity)
        {
            RepositoryHelper.Add(_context.ItemReferenceData, _context, entity);
        }

        public void Update(ItemReferenceData entity)
        {
            RepositoryHelper.Update(_context.ItemReferenceData, _context, entity);
        }

        public void Delete(Guid id)
        {
            RepositoryHelper.Delete(_context.ItemReferenceData, _context, id);
        }
    }
}