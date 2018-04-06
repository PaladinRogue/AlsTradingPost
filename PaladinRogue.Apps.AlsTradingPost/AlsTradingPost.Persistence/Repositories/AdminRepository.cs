using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public AdminRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Admin> Get()
        {
            return _context.Admins.AsNoTracking();
        }

        public Admin GetById(Guid id)
        {
            return _context.Admins.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public void Add(Admin admin)
        {
            _context.Admins.Add(admin);

            _context.SaveChanges();
        }

        public void Update(Admin obj)
        {
            try
            {
                _context.Admins.Update(obj);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(obj, e);
            }
        }

        public void Delete(Guid id)
        {
            var admin = GetById(id);

            try
            {
                _context.Admins.Remove(admin);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(admin, e);
            }
        }
    }
}