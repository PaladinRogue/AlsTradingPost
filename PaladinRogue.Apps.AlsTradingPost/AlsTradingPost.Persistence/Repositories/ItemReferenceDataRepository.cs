using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        
        public IEnumerable<ItemReferenceData> GetPage<TOrderByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Expression<Func<ItemReferenceData, TOrderByKey>> orderBy,
            bool orderByAscending,
            Expression<Func<ItemReferenceData, bool>> predicate = null)
        {
            return RepositoryHelper.GetPage(_context.ItemReferenceData, orderBy, orderByAscending, predicate, pageSize, pageOffset, out totalResults);
        }

        public IEnumerable<ItemReferenceData> GetPage<TOrderByKey, TThenByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Expression<Func<ItemReferenceData, TOrderByKey>> orderBy,
            bool orderByAscending = true,
            Expression<Func<ItemReferenceData, bool>> predicate = null,
            Expression<Func<ItemReferenceData, TThenByKey>> thenBy = null,
            bool? thenByAscending = null)
        {
            return RepositoryHelper.GetPage(_context.ItemReferenceData, orderBy, orderByAscending, predicate, thenBy, thenByAscending, pageSize, pageOffset, out totalResults);
        }
    }
}