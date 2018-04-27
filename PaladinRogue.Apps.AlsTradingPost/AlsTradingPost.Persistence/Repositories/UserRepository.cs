using System;
using System.Linq.Expressions;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Persistence.EntityFramework.Repositories;

namespace AlsTradingPost.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public UserRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }
        
        public User GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Users, id);
        }

        public User GetSingle(Expression<Func<User, bool>> predicate)
        {
            return RepositoryHelper.GetSingle(_context.Users, predicate);
        }

        public void Add(User entity)
        {
            RepositoryHelper.Add(_context.Users, _context, entity);
        }

        public void Update(User entity)
        {
            RepositoryHelper.Update(_context.Users, _context, entity);
        }
    }
}