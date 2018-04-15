using System.Collections.Generic;
using AlsTradingPost.Domain.Models.Interfaces;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Player : VersionedEntity, IPersona
    {
        public string DCI { get; set; }
        public List<Character> Characters { get; set; }
    }
}
