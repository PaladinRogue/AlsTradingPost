using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework.Infrastructure.Extensions
{
    public static class DbSetExtensions
    {
        public static DbSet<T> AddOrUpdate<T>(this DbSet<T> existingDbSet, IList<T> dataList)
            where T : class, IEntity
        {
            IList<T> existingList = existingDbSet.ToList();

            IList<T> itemsToAdd = dataList.Where(existing => existingList.All(i => i.Id != existing.Id)).ToList();
            if (itemsToAdd.Any())
            {
                existingDbSet.AddRange(itemsToAdd);
            }

            IList<T> itemsToUpdate = dataList.Except(existingList, new EntityComparer<T>()).Where(existing => existingList.Any(i => i.Id == existing.Id)).ToList();
            if (!itemsToUpdate.Any()) return existingDbSet;

            foreach (T item in itemsToUpdate)
            {
                T existingItem = existingList.Single(e => e.Id == item.Id);

                T entity = Mapper.Map(item, existingItem);

                existingDbSet.Update(entity);
            }

            return existingDbSet;
        }

        public static DbSet<T> AndDelete<T>(this DbSet<T> existingDbSet, IList<T> dataList)
            where T : class, IEntity
        {
            IList<T> existingList = existingDbSet.ToList();

            IList<T> itemsToDelete = existingList.Where(existing => dataList.All(i => i.Id != existing.Id)).ToList();
            if (itemsToDelete.Any())
            {
                existingDbSet.RemoveRange(itemsToDelete);
            }

            return existingDbSet;
        }
    }
}
