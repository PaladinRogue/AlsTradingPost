using System;
using System.Linq.Expressions;
using Common.Domain.Aggregates;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class, IAggregateRoot
    {
        protected readonly DbContext Context;

        public CommandRepository(DbContext context)
        {
            Context = context;
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="NotFoundDomainException"></exception>
        public virtual T GetWithConcurrencyCheck(Guid id, IConcurrencyVersion version)
        {
            return RepositoryHelper.GetWithConcurrencyCheck(Context.Set<T>(), id, version);
        }

        public T GetById(Guid id)
        {
            return RepositoryHelper.GetById(Context.Set<T>(), id);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return RepositoryHelper.GetSingle(Context.Set<T>(), predicate);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="UpdateDomainException"></exception>
        public virtual void Update(T entity)
        {
            RepositoryHelper.Update(Context.Set<T>(), Context, entity);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="CreateDomainException"></exception>
        public virtual void Add(T entity)
        {
            RepositoryHelper.Add(Context.Set<T>(), Context, entity);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="DeleteDomainException"></exception>
        public virtual void Delete(Guid id)
        {
            RepositoryHelper.Delete(Context.Set<T>(), Context, id);
        }
    }
}
