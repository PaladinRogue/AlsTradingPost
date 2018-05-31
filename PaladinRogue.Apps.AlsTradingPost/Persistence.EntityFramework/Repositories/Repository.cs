using System;
using System.Linq;
using System.Linq.Expressions;
using Common.Domain.Models.Interfaces;
using Common.Domain.Persistence;
using Common.Resources.Concurrency.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IVersionedEntity
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

        public IQueryable<T> GetPage<TOrderByKey>(int pageSize, int pageOffset, out int totalResults, Expression<Func<T, TOrderByKey>> orderBy,
            bool orderByAscending = true, Expression<Func<T, bool>> predicate = null)
        {
            return RepositoryHelper.GetPage(_context.Set<T>(), orderBy, orderByAscending, predicate, pageSize,
                pageOffset, out totalResults);
        }

        public IQueryable<T> GetPage<TOrderByKey, TThenByKey>(int pageSize, int pageOffset, out int totalResults, Expression<Func<T, TOrderByKey>> orderBy,
            bool orderByAscending = true, Expression<Func<T, bool>> predicate = null, Expression<Func<T, TThenByKey>> thenBy = null, bool? thenByAscending = null)
        {
            return RepositoryHelper.GetPage(_context.Set<T>(), orderBy, orderByAscending, predicate, thenBy, thenByAscending, pageSize,
                pageOffset, out totalResults);
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
