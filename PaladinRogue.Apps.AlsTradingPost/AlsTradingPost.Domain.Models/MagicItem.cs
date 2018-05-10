using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class MagicItem : VersionedEntity
    {
        public bool ForTrade { get; set; }
    }
}
