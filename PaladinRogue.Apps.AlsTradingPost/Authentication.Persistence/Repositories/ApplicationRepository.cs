using System;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Repositories;

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
	        return RepositoryHelper.GetById(_context.Applications.AsNoTracking(), id);
	    }

	    public Application GetSingle(Predicate<Application> predicate)
	    {
	        return RepositoryHelper.GetSingle(_context.Applications.AsNoTracking(), predicate);
	    }

	    public void Add(Application entity)
	    {
	        RepositoryHelper.Add(_context.Applications, _context, entity);
	    }

	    public void Update(Application entity)
	    {
	        RepositoryHelper.Update(_context.Applications, _context, entity);
	    }
    }
}