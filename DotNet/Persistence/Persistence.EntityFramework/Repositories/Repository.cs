using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Domain.Aggregates;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using Common.Resources.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Repositories
{
    public class Repository<T> : ICommandRepository<T>, IQueryRepository<T> where T : class, IAggregateRoot
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public T GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Set<T>(), id);
        }

        public IQueryable<T> Get(IList<SortBy> sort, Expression<Func<T, bool>> predicate = null)
        {
            return RepositoryHelper.Get(_context.Set<T>(), sort, predicate);
        }

        public IQueryable<T> GetPage(int pageSize, int pageOffset, out int totalResults, IList<SortBy> sort, Expression<Func<T, bool>> predicate = null)
        {
            return RepositoryHelper.GetPage(_context.Set<T>(), pageSize, pageOffset, out totalResults, sort, predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return RepositoryHelper.GetSingle(_context.Set<T>(), predicate);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="UpdateDomainException"></exception>
        public void Update(T entity)
        {
            RepositoryHelper.Update(_context.Set<T>(), _context, entity);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="CreateDomainException"></exception>
        public void Add(T entity)
        {
            RepositoryHelper.Add(_context.Set<T>(), _context, entity);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="DeleteDomainException"></exception>
        public void Delete(Guid id)
        {
            RepositoryHelper.Delete(_context.Set<T>(), _context, id);
        }

        public bool AreAny(Expression<Func<T, bool>> predicate)
        {
            return RepositoryHelper.CheckExists(_context.Set<T>(), predicate);
        }

        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="NotFoundDomainException"></exception>
        public T GetWithConcurrencyCheck(Guid id, IConcurrencyVersion version)
        {
            return RepositoryHelper.GetWithConcurrencyCheck(_context.Set<T>(), id, version);
        }
    }
}
