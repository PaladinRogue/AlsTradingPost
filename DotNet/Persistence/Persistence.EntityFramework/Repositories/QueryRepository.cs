using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Domain.Aggregates;
using Common.Domain.Models;
using Common.Domain.Persistence;
using Common.Resources.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class, IAggregateRoot
    {
        protected readonly DbContext Context;

        public QueryRepository(DbContext context)
        {
            Context = context;
        }

        public virtual Task<T> GetByIdAsync(Guid id)
        {
            return RepositoryHelper.GetByIdAsync(Context.Set<T>(), id);
        }

        public virtual Task<IQueryable<T>> GetAsync(IList<SortBy> sort, Expression<Func<T, bool>> predicate = null)
        {
            return RepositoryHelper.GetAsync(Context.Set<T>(), sort, predicate);
        }

        public virtual Task<IPagedResult<T>> GetPageAsync(int pageSize, int pageOffset, IList<SortBy> sort, Expression<Func<T, bool>> predicate = null)
        {
            return RepositoryHelper.GetPage(Context.Set<T>(), pageSize, pageOffset, sort, predicate);
        }

        public virtual Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return RepositoryHelper.GetSingleAsync(Context.Set<T>(), predicate);
        }

        public virtual Task<bool> AreAnyAsync(Expression<Func<T, bool>> predicate)
        {
            return RepositoryHelper.CheckExistsAsync(Context.Set<T>(), predicate);
        }
    }
}
