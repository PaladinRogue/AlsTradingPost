using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.Models
{
    public class Character : AggregateRoot, IOwnedAggregate
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        
        [MaxLength(20)]
        public string Race { get; set; }
        
        [MaxLength(20)]
        public string Class { get; set; }
        
        public byte Level { get; set; }

        public virtual Trader Trader { get; set; }

        public IAggregateOwner GetOwner()
        {
            return new AggregateOwner<Trader>(Trader.Id);
        }
    }
}