using System;
using System.Collections.Generic;
using System.Linq;
using Common.Domain.Exceptions;
using Common.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Repositories
{
    public static class RepositoryHelper
    {
        public static IQueryable<T> Filter<T>(IQueryable<T> results, Predicate<T> predicate)
        {
            if (predicate != null)
            {
                results = results.Where(i => predicate(i));
            }

            return results;
        }

        public static IOrderedQueryable<T> OrderBy<T, TOrderByKey>(IQueryable<T> results,
            Func<T, TOrderByKey> orderBy, bool orderByAscending)
        {
            if (orderBy != null)
            {
                results = orderByAscending ? results.OrderBy(x => orderBy(x)) : results.OrderByDescending(x => orderBy(x));
            }

            return (IOrderedQueryable<T>)results;
        }

        public static IOrderedQueryable<T> ThenBy<T, TThenByKey>(IOrderedQueryable<T> results,
            Func<T, TThenByKey> thenBy, bool thenByAscending)
        {
            if (thenBy != null)
            {
                results = thenByAscending ? results.ThenBy(x => thenBy(x)) : results.ThenByDescending(x => thenBy(x));
            }

            return results;
        }

        public static IEnumerable<T> GetPage<T>(IQueryable<T> results, int pageSize, int pageOffset, out int totalResults)
        {
            totalResults = results.Count();

            return results.Skip(pageOffset).Take(pageSize);
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

        public static T GetSingle<T>(IQueryable<T> results, Predicate<T> predicate)
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
    }
}
