using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        public Player(Guid id)
        {
            Id = id;
        }
        
        [MaxLength(50)]
        public string Alias { get; set; }
        
        [MaxLength(50)]
        public string DCI { get; set; }
        public IEnumerable<Character> Characters { get; set; }
    }
}
