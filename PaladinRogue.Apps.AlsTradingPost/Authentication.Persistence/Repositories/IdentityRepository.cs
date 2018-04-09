using System;
using System.Collections.Generic;
using System.Linq;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
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

        public void Add(Identity entity)
        {
            _context.Identities.Add(entity);

            _context.SaveChanges();
        }

        public void Update(Identity entity)
        {
            try
            {
                _context.Identities.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);

            try
            {
                _context.Identities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public Identity GetByAuthenticationId(string authenticationId)
        {
            return _context.Identities.AsNoTracking().FirstOrDefault(a => a.AuthenticationId == authenticationId);
        }
    }
}