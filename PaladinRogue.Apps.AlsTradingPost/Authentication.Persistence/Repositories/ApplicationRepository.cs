using System;
using System.Linq;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
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

        public Application GetById(Guid id)
        {
            try
            {
                return _context.Applications.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

	    public Application GetSingle(Predicate<Application> predicate)
	    {
	        try
	        {
	            return _context.Applications.AsNoTracking().SingleOrDefault(a => predicate(a));
	        }
	        catch (InvalidOperationException)
	        {
	            throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
	        }
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
	}
}