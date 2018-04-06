using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlsTradingPost.Persistence.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly AlsTradingPostDbContext _context;

        public CharacterRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Character> Get()
        {
            return _context.Characters.AsNoTracking();
        }

        public Character GetById(Guid id)
        {
            try
            {
                return _context.Characters.AsNoTracking().SingleOrDefault(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new DomainException("Multiple entites exist with given Id");
            }
        }

        public Character GetSingle(Predicate<Character> predicate)
        {
            try
            {
                return _context.Characters.AsNoTracking().SingleOrDefault(a => predicate(a));
            }
            catch (InvalidOperationException)
            {
                throw new DomainException($"Multiple entites exist which match given predicate ({ predicate })");
            }
        }

        public void Add(Character entity)
        {
            _context.Characters.Add(entity);

            _context.SaveChanges();
        }

        public void Update(Character entity)
        {
            try
            {
                _context.Characters.Update(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }

        public void Delete(Guid id)
        {
            Character entity = GetById(id);

            try
            {
                _context.Characters.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyDomainException(entity, e);
            }
        }
    }
}