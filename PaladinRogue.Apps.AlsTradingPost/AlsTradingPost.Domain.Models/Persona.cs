using System;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public abstract class Persona : Entity
    {
        public Guid Identity { get; set; }
        public virtual PersonalDetails PersonalDetails { get; set; }
    }
}
