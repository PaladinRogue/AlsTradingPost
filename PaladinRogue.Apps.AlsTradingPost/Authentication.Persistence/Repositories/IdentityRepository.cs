using System;
using System.Collections.Generic;
using System.Linq;
using Authentication.Domain.Models;
using Authentication.Persistence.Interfaces;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

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
            return _context.Identities.AsNoTracking();
        }

        public Identity GetById(Guid id)
        {
            return _context.Identities.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public void Add(Identity identity)
        {
            _context.Identities.Add(identity);

            _context.SaveChanges();
        }

        public void Update(Identity obj)
        {
            try
            {
                _context.Identities.Update(obj);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(obj, e);
            }
        }

        public void Delete(Guid id)
        {
            var identity = GetById(id);

            try
            {
                _context.Identities.Remove(identity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(identity, e);
            }
        }
    }
}