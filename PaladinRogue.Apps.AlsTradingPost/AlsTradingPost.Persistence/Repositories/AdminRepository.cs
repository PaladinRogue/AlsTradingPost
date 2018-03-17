using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Persistence.Interfaces;

namespace AlsTradingPost.Persistence.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public AdminRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IList<Admin> Get()
        {
            return _context.Admins.ToList();
        }

        public Admin GetById(Guid id)
        {
            return _context.Admins.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Admin admin)
        {
            _context.Admins.Add(admin);

            _context.SaveChanges();
        }

        public void Update(Admin obj)
        {
            _context.Admins.Update(obj);

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}