using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Domain.Aggregates;
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

        public virtual T GetById(Guid id)
        {
            return RepositoryHelper.GetById(Context.Set<T>().AsNoTracking(), id);
        }

        public virtual IQueryable<T> Get(IList<SortBy> sort, Expression<Func<T, bool>> predicate = null)
        {
            return RepositoryHelper.Get(Context.Set<T>().AsNoTracking(), sort, predicate);
        }

        public virtual IQueryable<T> GetPage(int pageSize, int pageOffset, out int totalResults, IList<SortBy> sort, Expression<Func<T, bool>> predicate = null)
        {
            return RepositoryHelper.GetPage(Context.Set<T>().AsNoTracking(), pageSize, pageOffset, out totalResults, sort, predicate);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return RepositoryHelper.GetSingle(Context.Set<T>().AsNoTracking(), predicate);
        }

        public virtual bool AreAny(Expression<Func<T, bool>> predicate)
        {
            return RepositoryHelper.CheckExists(Context.Set<T>().AsNoTracking(), predicate);
        }
    }
}
