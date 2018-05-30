using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.Models
{
    public class MagicItem : AggregateRoot, IOwnedAggregate
    {
        public bool ForTrade { get; set; }

        public virtual Character Character { get; set; }
        
        public virtual MagicItemTemplate MagicItemTemplate { get; set; }

        public IAggregateOwner GetOwner()
        {
            return new AggregateOwner<Trader>(Character.Trader.Id);
        }
    }
}
