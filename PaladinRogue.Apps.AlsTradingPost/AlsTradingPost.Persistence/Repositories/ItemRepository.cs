using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Persistence.Interfaces;

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
            throw new NotImplementedException();
        }

        public Item GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(Item item)
        {
            _context.Items.Add(item);
        }

        public void Update(Item obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}