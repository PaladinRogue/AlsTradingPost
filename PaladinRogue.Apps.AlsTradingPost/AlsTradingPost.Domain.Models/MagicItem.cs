using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class MagicItem : AggregateRoot
    {
        public bool ForTrade { get; set; }

        public virtual Character Character { get; set; }
        
        public virtual MagicItemTemplate MagicItemTemplate { get; set; }
    }
}
