using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Resources;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Player : VersionedEntity, IPersona
    {
        public PersonaType TypeDiscriminator => PersonaType.Player;
        
        public Player()
        {
        }
        
        public Player(Guid userId)
        {
            Id = userId;
        }
        
        public string DCI { get; set; }
        public IEnumerable<Character> Characters { get; set; }
    }
}
