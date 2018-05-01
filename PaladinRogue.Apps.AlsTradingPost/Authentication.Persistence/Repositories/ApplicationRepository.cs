using System;
using System.Linq.Expressions;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
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
	        return RepositoryHelper.GetById(_context.Applications, id);
	    }

	    public Application GetSingle(Expression<Func<Application, bool>> predicate)
	    {
	        return RepositoryHelper.GetSingle(_context.Applications, predicate);
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