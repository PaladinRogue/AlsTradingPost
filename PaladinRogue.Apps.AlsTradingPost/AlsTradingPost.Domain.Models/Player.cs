using System.Collections.Generic;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Player : Persona
    {
        public string DCI { get; set; }
        public List<Character> Characters { get; set; }
    }
}
