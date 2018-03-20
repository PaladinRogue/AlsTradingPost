using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Persistence.Interfaces;

namespace AlsTradingPost.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public IdentityRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Identity> Get()
        {
            throw new NotImplementedException();
        }

        public Identity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(Identity identity)
        {
            _context.Identities.Add(identity);
        }

        public void Update(Identity obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}