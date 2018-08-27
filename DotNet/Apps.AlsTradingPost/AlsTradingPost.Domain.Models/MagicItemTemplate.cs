using System.ComponentModel.DataAnnotations;
using AlsTradingPost.Domain.Models.Constants;
using Common.Domain.Models;
using Common.Domain.Sorting;

namespace AlsTradingPost.Domain.Models
{
    public class MagicItemTemplate : AggregateRoot
    {
        [MaxLength(100)]
        [Sortable]
        public string Name { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public RarityType Rarity { get; set; }

        public bool Verified { get; set; }
    }
}
