using System.Collections.Generic;
using AlsTradingPost.Domain.Models.Interfaces;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.Models
{
    public class Player : Entity, IPersona
    {
        public string DCI { get; set; }
        public List<Character> Characters { get; set; }
    }
}
