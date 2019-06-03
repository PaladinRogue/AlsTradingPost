using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.ApplicationServices.Concurrency.Interfaces;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;
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

        public void Update(T entity)
        {
            RepositoryHelper.Update(_context.Set<T>(), _context, entity);
        }

        public void Add(T entity)
        {
            RepositoryHelper.Add(_context.Set<T>(), _context, entity);
        }

        public void Delete(Guid id)
        {
            RepositoryHelper.Delete(_context.Set<T>(), _context, id);
        }

        public bool CheckExists(Guid id)
        {
            return RepositoryHelper.CheckExists(_context.Set<T>(), id);
        }

        public bool CheckConcurrency(Guid id, IConcurrencyVersion version)
        {
            return RepositoryHelper.CheckConcurrency(_context.Set<T>(), id, version.Version);
        }
    }
}
