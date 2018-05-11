using System;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Resources;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class Admin : AggregateRoot, IPersona
    {
        public PersonaType TypeDiscriminator => PersonaType.Admin;
        
        public Admin()
        {
        }
        
        public Admin(Guid id)
        {
            Id = id;
        }
    }
}
