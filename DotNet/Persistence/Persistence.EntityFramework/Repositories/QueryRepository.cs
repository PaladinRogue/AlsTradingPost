using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaladinRogue.Libray.Core.Common.Sorting;
using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Models;
using PaladinRogue.Libray.Core.Domain.Persistence;

namespace PaladinRogue.Libray.Persistence.EntityFramework.Repositories
{
    public class QueryRepository<T> : QueryRepository<T, DbContext> where T : class, IAggregateRoot
    {
        public QueryRepository(DbContext context) : base(context)
        {
        }
    }

    public class QueryRepository<T, TDbContext> : IQueryRepository<T>
        where T : class, IAggregateRoot
        where TDbContext : DbContext
    {
        protected readonly TDbContext Context;

        public QueryRepository(TDbContext context)
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
