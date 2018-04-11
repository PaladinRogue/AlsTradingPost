using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class UserPersona : Entity
    {
        public virtual Persona Persona { get; set; }
        public virtual User User { get; set; }
    }
}
