using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Entities;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Common.Resources.Extensions;
using Common.Resources.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Repositories
{
    public static class RepositoryHelper
    {
        public static Task<IQueryable<T>> GetAsync<T>(
            IQueryable<T> results,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null) where T : IEntity
        {
            IQueryable<T> filteredResults = Filter(results, predicate);
            if (sort == null || !sort.Any()) return Task.FromResult(filteredResults);

            SortBy firstSort = sort.First();
            IOrderedQueryable<T> orderedResults = OrderBy(filteredResults, firstSort.PropertyName.CreatePropertyAccessor<T, object>(), firstSort.IsAscending);
            orderedResults = sort.Skip(1).Aggregate(orderedResults, (current, sortBy) => ThenBy(current, sortBy.PropertyName.CreatePropertyAccessor<T, object>(), sortBy.IsAscending));

            return Task.FromResult((IQueryable<T>) orderedResults);
        }

        public static Task<IPagedResult<T>> GetPage<T>(
            IQueryable<T> results,
            int pageSize, int pageOffset,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null) where T : IEntity
        {
            IQueryable<T> filteredResults = Filter(results, predicate);
            if (sort == null || !sort.Any()) return GetPageAsync(filteredResults, pageSize, pageOffset);

            SortBy firstSort = sort.First();
            IOrderedQueryable<T> orderedResults = OrderBy(filteredResults, firstSort.PropertyName.CreatePropertyAccessor<T, object>(), firstSort.IsAscending);
            orderedResults = sort.Skip(1).Aggregate(orderedResults, (current, sortBy) => ThenBy(current, sortBy.PropertyName.CreatePropertyAccessor<T, object>(), sortBy.IsAscending));

            return GetPageAsync(orderedResults ?? filteredResults, pageSize, pageOffset);
        }

        public static Task<bool> CheckExistsAsync<T>(IQueryable<T> results, Expression<Func<T, bool>> predicate) where T : IEntity
        {
            return Filter(results, predicate).AnyAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="results"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="NotFoundDomainException"></exception>
        public static async Task<T> GetWithConcurrencyCheckAsync<T>(IQueryable<T> results, Guid id, IConcurrencyVersion version) where T : IVersionedEntity
        {
            if (version == null)
            {
                throw new ConcurrencyDomainException();
            }

            try
            {
                T versionedEntity = await results.SingleOrDefaultAsync(e => e.Id == id);

                if (versionedEntity == null)
                {
                    throw new NotFoundDomainException($"Entity of type: {nameof(T)} not found with Id: {id}");
                }

                if (versionedEntity.Version != version.Version)
                {
                    throw new ConcurrencyDomainException(typeof(T), id, version);
                }

                return versionedEntity;
            }
            catch (InvalidOperationException)
            {
                throw new ConcurrencyDomainException(typeof(T), id, version);
            }
        }

        public static async Task<T> GetByIdAsync<T>(IQueryable<T> results, Guid id) where T : IEntity
        {
            try
            {
                return await Filter(results, e => e.Id == id).SingleOrDefaultAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new GetByIdDomainException(id, ex);
            }
        }

        public static async Task<T> GetSingleAsync<T>(IQueryable<T> results, Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await Filter(results, predicate).SingleOrDefaultAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new GetSingleDomainException<T>(predicate, ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="CreateDomainException"></exception>
        public static async Task AddAsync<T>(DbSet<T> dbSet, DbContext context, T entity) where T : class, IVersionedEntity
        {
            try
            {
                entity.UpdateVersion();

                await dbSet.AddAsync(entity);

                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
            catch (DbUpdateException e)
            {
                throw new CreateDomainException(entity, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="UpdateDomainException"></exception>
        public static async Task UpdateAsync<T>(DbSet<T> dbSet, DbContext context, T entity) where T : class, IVersionedEntity
        {
            try
            {
                entity.UpdateVersion();

                dbSet.Update(entity);

                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
            catch (DbUpdateException e)
            {
                throw new UpdateDomainException(entity, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ConcurrencyDomainException"></exception>
        /// <exception cref="DeleteDomainException"></exception>
        public static async Task DeleteAsync<T>(DbSet<T> dbSet, DbContext context, Guid id) where T : class, IVersionedEntity
        {
            T entity = await GetByIdAsync(dbSet, id);

            try
            {
                dbSet.Remove(entity);

                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
            catch (DbUpdateException e)
            {
                throw new DeleteDomainException(entity, e);
            }
        }

        private static IQueryable<T> Filter<T>(IQueryable<T> results, Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? results.Where(predicate) : results;
        }

        private static IOrderedQueryable<T> OrderBy<T, TOrderByKey>(IQueryable<T> results,
            Expression<Func<T, TOrderByKey>> orderBy, bool orderByAscending)
        {
            if (orderBy != null)
            {
                return orderByAscending ? results.OrderBy(orderBy) : results.OrderByDescending(orderBy);
            }

            return (IOrderedQueryable<T>) results;
        }

        private static IOrderedQueryable<T> ThenBy<T, TThenByKey>(IOrderedQueryable<T> results,
            Expression<Func<T, TThenByKey>> thenBy, bool? thenByAscending)
        {
            if (thenBy != null)
            {
                return thenByAscending.HasValue && thenByAscending.Value ? results.ThenBy(thenBy) : results.ThenByDescending(thenBy);
            }

            return results;
        }

        private static async Task<IPagedResult<T>> GetPageAsync<T>(IQueryable<T> results, int pageSize, int pageOffset) where T : IEntity
        {
            return PagedResult<T>.Create(results.Skip(pageOffset).Take(pageSize), await results.CountAsync());
        }
    }
}
