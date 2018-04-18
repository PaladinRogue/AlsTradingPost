using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Repositories;

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

        public IOrderedQueryable<Item> Get<TOrderByKey>(
            Predicate<Item> predicate = null,
            Func<Item, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.OrderBy(Get(predicate), orderBy, orderByAscending);
        }

        public IOrderedQueryable<Item> Get<TOrderByKey, TThenByKey>(
            Predicate<Item> predicate = null,
            Func<Item, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Item, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.ThenBy(Get(predicate, orderBy, orderByAscending), thenBy, thenByAscending);
        }

        public IEnumerable<Item> GetPage(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Item> predicate = null)
        {
            return RepositoryHelper.GetPage(Get(predicate), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<Item> GetPage<TOrderByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Item> predicate = null,
            Func<Item, TOrderByKey> orderBy = null,
            bool orderByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending), pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<Item> GetPage<TOrderByKey, TThenByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Predicate<Item> predicate = null,
            Func<Item, TOrderByKey> orderBy = null,
            bool orderByAscending = true,
            Func<Item, TThenByKey> thenBy = null,
            bool thenByAscending = true)
        {
            return RepositoryHelper.GetPage(Get(predicate, orderBy, orderByAscending, thenBy, thenByAscending), pageSize, pageOffset, out totalResults);
        }

        public Item GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Items.AsNoTracking(), id);
        }

        public Item GetSingle(Predicate<Item> predicate)
        {
            return RepositoryHelper.GetSingle(_context.Items.AsNoTracking(), predicate);
        }

        public void Add(Item entity)
        {
            RepositoryHelper.Add(_context.Items, _context, entity);
        }

        public void Update(Item entity)
        {
            RepositoryHelper.Update(_context.Items, _context, entity);
        }

        public void Delete(Guid id)
        {
            RepositoryHelper.Delete(_context.Items, _context, id);
        }
    }
}