using System.Collections.Generic;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Character : VersionedEntity
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public virtual IEnumerable<Item> Items { get; set; }
    }
}