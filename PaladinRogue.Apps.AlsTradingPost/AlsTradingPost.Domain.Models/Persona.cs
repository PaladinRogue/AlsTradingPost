using AlsTradingPost.Domain.Models.Enums;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Persona : Entity
    {
        public PersonaType PersonaType { get; set; }
        public string Name { get; set; }
    }
}
