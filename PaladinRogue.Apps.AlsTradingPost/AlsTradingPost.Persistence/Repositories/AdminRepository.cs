using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Persistence.EntityFramework.Repositories;

namespace AlsTradingPost.Persistence.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public AdminRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public Admin GetById(Guid id)
        {
            return RepositoryHelper.GetById(_context.Admins, id);
        }

        public void Add(Admin entity)
        {
            RepositoryHelper.Add(_context.Admins, _context, entity);
        }

        public void Update(Admin entity)
        {
            RepositoryHelper.Update(_context.Admins, _context, entity);
        }
    }
}