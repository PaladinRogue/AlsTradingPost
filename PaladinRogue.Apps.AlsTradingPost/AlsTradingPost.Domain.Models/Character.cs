using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Character : AggregateRoot
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
    }
}