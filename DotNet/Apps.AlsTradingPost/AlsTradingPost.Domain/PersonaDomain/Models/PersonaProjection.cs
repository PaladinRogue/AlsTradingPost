using System;
using AlsTradingPost.Resources;

namespace AlsTradingPost.Domain.PersonaDomain.Models
{
    public class PersonaProjection
    {
        public Guid Id { get; set; }
        public PersonaType PersonaType { get; set; }
    }
}