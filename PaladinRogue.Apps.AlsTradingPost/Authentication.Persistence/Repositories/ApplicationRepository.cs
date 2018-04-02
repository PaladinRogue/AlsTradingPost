using System;
using System.Collections.Generic;
using System.Linq;
using Authentication.Domain.Models;
using Authentication.Persistence.Interfaces;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.Repositories
{
    public class ApplicationRepository : IApplicationRepository
	{
        private readonly AuthenticationDbContext _context;

        public ApplicationRepository(AuthenticationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Application> Get()
        {
            return _context.Applications.AsNoTracking();
        }

        public Application GetById(Guid id)
        {
            return _context.Applications.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public void Add(Application entity)
        {
            _context.Applications.Add(entity);

            _context.SaveChanges();
        }

        public void Update(Application entity)
        {
            try
            {
                _context.Applications.Update(entity);

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
                _context.Applications.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}