using System;
using System.Linq;
using System.Linq.Expressions;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Repositories;

namespace AlsTradingPost.Persistence.Repositories
{
    public class MagicItemTemplateRepository : IMagicItemTemplateRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public MagicItemTemplateRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }
        
        public IQueryable<MagicItemTemplate> GetPage<TOrderByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Expression<Func<MagicItemTemplate, TOrderByKey>> orderBy,
            bool orderByAscending,
            Expression<Func<MagicItemTemplate, bool>> predicate = null)
        {
            return RepositoryHelper.GetPage(_context.MagicItemTemplates.AsNoTracking(), orderBy, orderByAscending, predicate, pageSize, pageOffset, out totalResults);
        }

        public IQueryable<MagicItemTemplate> GetPage<TOrderByKey, TThenByKey>(
            int pageSize,
            int pageOffset,
            out int totalResults,
            Expression<Func<MagicItemTemplate, TOrderByKey>> orderBy,
            bool orderByAscending = true,
            Expression<Func<MagicItemTemplate, bool>> predicate = null,
            Expression<Func<MagicItemTemplate, TThenByKey>> thenBy = null,
            bool? thenByAscending = null)
        {
            return RepositoryHelper.GetPage(_context.MagicItemTemplates.AsNoTracking(), orderBy, orderByAscending, predicate, thenBy, thenByAscending, pageSize, pageOffset, out totalResults);
        }
    }
}