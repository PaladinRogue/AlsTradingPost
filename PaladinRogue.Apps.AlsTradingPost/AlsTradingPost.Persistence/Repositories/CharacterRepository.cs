using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;

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
            throw new NotImplementedException();
        }

        public Character GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(Character character)
        {
            _context.Characters.Add(character);
        }

        public void Update(Character obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}