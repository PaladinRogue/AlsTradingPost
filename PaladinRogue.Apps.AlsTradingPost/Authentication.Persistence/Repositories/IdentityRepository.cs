using System;
using System.Linq.Expressions;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Repositories;

namespace Authentication.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly AuthenticationDbContext _context;

        public IdentityRepository(AuthenticationDbContext context)
        {
            _context = context;
        }

        public Identity GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Identities.AsNoTracking(), id);
        }

        public Identity GetSingle(Expression<Func<Identity, bool>> predicate)
        {
            return RepositoryHelper.GetSingle(_context.Identities.AsNoTracking(), predicate);
        }

        public void Add(Identity entity)
        {
            RepositoryHelper.Add(_context.Identities, _context, entity);
        }

        public void Update(Identity entity)
        {
            RepositoryHelper.Update(_context.Identities, _context, entity);
        }
    }
}