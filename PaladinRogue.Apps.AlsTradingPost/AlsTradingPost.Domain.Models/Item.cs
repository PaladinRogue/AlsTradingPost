using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Item : VersionedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }
        public bool ForTrade { get; set; }
        public Character Character { get; set; }
    }
}
