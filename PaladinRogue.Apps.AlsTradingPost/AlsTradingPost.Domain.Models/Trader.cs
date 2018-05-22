using System;
using System.ComponentModel.DataAnnotations;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Resources;
using Common.Domain.Models;
using Common.Domain.Models.DataProtection;

namespace AlsTradingPost.Domain.Models
{
    public class Trader : AggregateRoot, IPersona
    {
        public PersonaType TypeDiscriminator => PersonaType.Trader;
        
        public Trader()
        {
        }
        
        public Trader(Guid id)
        {
            Id = id;
        }
        
        [Required]
        [MaxLength(100)]
        [SensitiveInformation]
        public string Alias { get; set; }
        
        [Required]
        [MaxLength(100)]
        [SensitiveInformation]
        public string DCI { get; set; }
    }
}
