using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public ItemRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Item> Get()
        {
            return _context.Items.AsNoTracking();
        }

        public Item GetById(Guid id)
        {
            try
            {
                return _context.Items.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public Item GetSingle(Predicate<Item> predicate)
        {
            try
            {
                return _context.Items.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(Item entity)
        {
            _context.Items.Add(entity);

            _context.SaveChanges();
        }

        public void Update(Item entity)
        {
            try
            {
                _context.Items.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            Item entity = GetById(id);

            try
            {
                _context.Items.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}