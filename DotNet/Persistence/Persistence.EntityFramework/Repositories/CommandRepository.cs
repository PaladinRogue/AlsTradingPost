using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Domain.Aggregates;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using Common.Resources.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Repositories
{
    public class CommandRepository<T> : CommandRepository<T, DbContext> where T : class, IAggregateRoot
    {
        public CommandRepository(DbContext context) : base(context)
        {
        }
    }

    public class CommandRepository<T, TDbContext> : ICommandRepository<T>
        where T : class, IAggregateRoot
        where TDbContext : DbContext
    {
        protected readonly TDbContext Context;

        public CommandRepository(TDbContext context)
        {
            Context = context;
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="NotFoundDomainException"></exception>
        public virtual Task<T> GetWithConcurrencyCheckAsync(Guid id, IConcurrencyVersion version)
        {
            return RepositoryHelper.GetWithConcurrencyCheckAsync(Context.Set<T>(), id, version);
        }

        public virtual Task<T> GetByIdAsync(Guid id)
        {
            return RepositoryHelper.GetByIdAsync(Context.Set<T>(), id);
        }

        public virtual Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return RepositoryHelper.GetSingleAsync(Context.Set<T>(), predicate);
        }

        /// <exception cref="NotImplementedException"></exception>
        public Task<IQueryable<T>> GetAsync(IList<SortBy> sort = null, Expression<Func<T, bool>> predicate = null)
        {
            return RepositoryHelper.GetAsync(Context.Set<T>(), sort, predicate);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="UpdateDomainException"></exception>
        public virtual Task UpdateAsync(T entity)
        {
           return RepositoryHelper.UpdateAsync(Context.Set<T>(), Context, entity);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="CreateDomainException"></exception>
        public virtual Task AddAsync(T entity)
        {
            return RepositoryHelper.AddAsync(Context.Set<T>(), Context, entity);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="DeleteDomainException"></exception>
        public virtual Task DeleteAsync(Guid id)
        {
           return RepositoryHelper.DeleteAsync(Context.Set<T>(), Context, id);
        }
    }
}
