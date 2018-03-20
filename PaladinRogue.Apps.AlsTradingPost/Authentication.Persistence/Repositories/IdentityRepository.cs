using System;
using System.Collections.Generic;
using Authentication.Domain.Models;
using Authentication.Persistence.Interfaces;

namespace Authentication.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly AuthenticationDbContext _context;

        public IdentityRepository(AuthenticationDbContext context)
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