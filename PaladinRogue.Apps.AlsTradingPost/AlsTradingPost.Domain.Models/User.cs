using System;
using System.Collections.Generic;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class User : Entity
    {
        public Guid IdentityId { get; set; }
        public string KnownAs { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public virtual IEnumerable<UserPersona> UserPersonas { get; set; }
    }
}
