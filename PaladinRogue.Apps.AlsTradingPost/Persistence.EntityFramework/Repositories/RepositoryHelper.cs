using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Domain.Exceptions;
using Common.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Repositories
{
    public static class RepositoryHelper
    {
        public static IEnumerable<T> GetPage<T, TOrderByKey>(
            IQueryable<T> results,
            Expression<Func<T, TOrderByKey>> orderBy,
            bool orderByAscending,
            Expression<Func<T, bool>> predicate,
            int pageSize, int pageOffset, out int totalResults) where T : class
        {
            
            IQueryable<T> filteredResults = Filter(results, predicate);
            IQueryable<T> orderedResults = OrderBy(filteredResults, orderBy, orderByAscending);

            return GetPage(orderedResults, pageSize, pageOffset, out totalResults);
        }

        public static IEnumerable<T> GetPage<T, TOrderByKey, TThenByKey>(
            IQueryable<T> results,
            Expression<Func<T, TOrderByKey>> orderBy,
            bool orderByAscending,
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TThenByKey>> thenBy,
            bool? thenByAscending,
            int pageSize, int pageOffset, out int totalResults) where T : class
        {
            
            IQueryable<T> filteredResults = Filter(results, predicate);
            IOrderedQueryable<T> orderedResults = OrderBy(filteredResults, orderBy, orderByAscending);
            IOrderedQueryable<T> thenByOrderedResults = ThenBy(orderedResults, thenBy, thenByAscending);

            return GetPage(thenByOrderedResults, pageSize, pageOffset, out totalResults);
        }

        public static T GetById<T>(IQueryable<T> results, Guid id) where T : IEntity
        {
            try
            {
                return Filter(results, e => e.Id == id).SingleOrDefault();
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public static T GetSingle<T>(IQueryable<T> results, Expression<Func<T, bool>> predicate)
        {
            try
            {
                return Filter(results, predicate).SingleOrDefault();
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public static void AddUnVersioned<T>(DbSet<T> dbSet, DbContext context, T entity) where T : class, IEntity
        {
            try
            {
                dbSet.Add(entity);

                context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new CreateDomainException(entity, e);
            }
        }

        public static void Add<T>(DbSet<T> dbSet, DbContext context, T entity) where T : class, IVersionedEntity
        {
            try
            {
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
        
        private static IEnumerable<T> GetPage<T>(IQueryable<T> results, int pageSize, int pageOffset, out int totalResults) where T : class
        {
            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
        }
    }
}
