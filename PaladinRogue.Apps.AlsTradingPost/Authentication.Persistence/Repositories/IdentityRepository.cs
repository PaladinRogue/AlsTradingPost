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
            try
            {
                return _context.Identities.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public Identity GetSingle(Predicate<Identity> predicate)
        {
            try
            {
                return _context.Identities.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
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
    }
}