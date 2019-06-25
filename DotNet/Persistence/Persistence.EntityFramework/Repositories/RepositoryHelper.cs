using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Exceptions;
using Common.Domain.Models.Interfaces;
using Common.Resources.Extensions;
using Common.Resources.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Repositories
{
    public static class RepositoryHelper
    {
        public static IQueryable<T> Get<T>(
            IQueryable<T> results,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null) where T : IEntity
        {
            IQueryable<T> filteredResults = Filter(results, predicate);
            if (sort == null || !sort.Any()) return filteredResults;

            SortBy firstSort = sort.First();
            IOrderedQueryable<T> orderedResults = OrderBy(filteredResults, firstSort.PropertyName.CreatePropertyAccessor<T, object>(), firstSort.IsAscending);
            orderedResults = sort.Skip(1).Aggregate(orderedResults, (current, sortBy) => ThenBy(current, sortBy.PropertyName.CreatePropertyAccessor<T, object>(), sortBy.IsAscending));

            return orderedResults;
        }

        public static IQueryable<T> GetPage<T>(
            IQueryable<T> results,
            int pageSize, int pageOffset, out int totalResults,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null) where T : IEntity
        {
            IQueryable<T> filteredResults = Filter(results, predicate);
            if (sort == null || !sort.Any()) return GetPage(filteredResults, pageSize, pageOffset, out totalResults);

            SortBy firstSort = sort.First();
            IOrderedQueryable<T> orderedResults = OrderBy(filteredResults, firstSort.PropertyName.CreatePropertyAccessor<T, object>(), firstSort.IsAscending);
            orderedResults = sort.Skip(1).Aggregate(orderedResults, (current, sortBy) => ThenBy(current, sortBy.PropertyName.CreatePropertyAccessor<T, object>(), sortBy.IsAscending));

            return GetPage(orderedResults ?? filteredResults, pageSize, pageOffset, out totalResults);
        }


        public static bool CheckExists<T>(IQueryable<T> results, Expression<Func<T, bool>> predicate) where T : IEntity
        {
            return Filter(results, predicate).Any();
        }

        public static T GetWithConcurrencyCheck<T>(IQueryable<T> results, Guid id, IConcurrencyVersion version) where T : IVersionedEntity
        {
            if (version == null)
            {
                throw new ConcurrencyDomainException();
            }

            try
            {
                return results.Single(e => e.Id == id && e.Version == version.Version);
            }
            catch (InvalidOperationException)
            {
                throw new ConcurrencyDomainException(typeof(T), id, version);
            }
        }

        public static T GetById<T>(IQueryable<T> results, Guid id) where T : IEntity
        {
            try
            {
                return Filter(results, e => e.Id == id).SingleOrDefault();
            }
            catch (InvalidOperationException ex)
            {
                throw new GetByIdDomainException(id, ex);
            }
        }

        public static T GetSingle<T>(IQueryable<T> results, Expression<Func<T, bool>> predicate)
        {
            try
            {
                return Filter(results, predicate).SingleOrDefault();
            }
            catch (InvalidOperationException ex)
            {
                throw new GetSingleDomainException<T>(predicate, ex);
            }
        }

        public static void Add<T>(DbSet<T> dbSet, DbContext context, T entity) where T : class, IVersionedEntity
        {
            try
            {
                entity.UpdateVersion();

                dbSet.Add(entity);

                context.SaveChanges();
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

        public static void Update<T>(DbSet<T> dbSet, DbContext context, T entity) where T : class, IVersionedEntity
        {
            try
            {
                entity.UpdateVersion();

                dbSet.Update(entity);

                context.SaveChanges();
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

        public static void Delete<T>(DbSet<T> dbSet, DbContext context, Guid id) where T : class, IVersionedEntity
        {
            T entity = GetById(dbSet, id);

            try
            {
                dbSet.Remove(entity);

                context.SaveChanges();
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

        private static IQueryable<T> GetPage<T>(IQueryable<T> results, int pageSize, int pageOffset, out int totalResults) where T : IEntity
        {
            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }
    }
}
