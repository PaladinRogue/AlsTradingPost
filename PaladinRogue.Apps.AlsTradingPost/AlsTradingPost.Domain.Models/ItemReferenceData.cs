using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class ItemReferenceData : Entity
    {
        public string Name { get; set; }

        public bool? Verified { get; set; }
    }
}
