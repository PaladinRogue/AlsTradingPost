using System;
using AlsTradingPost.Resources;
using Common.Domain.Concurrency;

namespace AlsTradingPost.Domain.UserDomain.Models
{
    public class UpdateUserDdto : VersionedDdto
    {
        public Guid Id { get; set; }
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public Persona Personas { get; set; }
    }
}
