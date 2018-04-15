using System;
using AlsTradingPost.Resources;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class User : VersionedEntity
    {
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public virtual Persona Personas { get; set; }
    }
}
