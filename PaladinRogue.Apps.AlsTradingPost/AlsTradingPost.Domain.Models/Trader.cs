using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Resources;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Trader : VersionedEntity, IPersona
    {
        public PersonaType TypeDiscriminator => PersonaType.Trader;
        
        public Trader()
        {
        }
        
        public Trader(Guid id)
        {
            Id = id;
        }
        
        [MaxLength(50)]
        public string Alias { get; set; }
        
        [MaxLength(50)]
        public string DCI { get; set; }
        public virtual IEnumerable<Character> Characters { get; set; }
    }
}
