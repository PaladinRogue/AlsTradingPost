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
            try
            {
                return _context.Admins.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public Admin GetSingle(Predicate<Admin> predicate)
        {
            try
            {
                return _context.Admins.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(Admin entity)
        {
            _context.Admins.Add(entity);

            _context.SaveChanges();
        }

        public void Update(Admin entity)
        {
            try
            {
                _context.Admins.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            Admin entity = GetById(id);

            try
            {
                _context.Admins.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}