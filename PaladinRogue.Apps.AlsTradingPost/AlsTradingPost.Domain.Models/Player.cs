using System.Collections.Generic;
using AlsTradingPost.Domain.Models.Base;

namespace AlsTradingPost.Domain.Models
{
    public class Player : User
    {
        public string DCI { get; set; }
        public List<Character> Characters { get; set; }
    }
}
