using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public UserRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> Get()
        {
            return _context.Users.AsNoTracking();
        }

        public User GetById(Guid id)
        {
            try
            {
                return _context.Users.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public User GetSingle(Predicate<User> predicate)
        {
            try
            {
                return _context.Users.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);

            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            try
            {
                _context.Users.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            User entity = GetById(id);

            try
            {
                _context.Users.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}