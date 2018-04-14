using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence.Repositories
{
    public class ItemReferenceDataRepository : IItemReferenceDataRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public ItemReferenceDataRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ItemReferenceData> Get()
        {
            return _context.ItemReferenceData.AsNoTracking();
        }

        public ItemReferenceData GetById(Guid id)
        {
            try
            {
                return _context.ItemReferenceData.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public ItemReferenceData GetSingle(Predicate<ItemReferenceData> predicate)
        {
            try
            {
                return _context.ItemReferenceData.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(ItemReferenceData entity)
        {
            _context.ItemReferenceData.Add(entity);

            _context.SaveChanges();
        }

        public void Update(ItemReferenceData entity)
        {
            try
            {
                _context.ItemReferenceData.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            ItemReferenceData entity = GetById(id);

            try
            {
                _context.ItemReferenceData.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}