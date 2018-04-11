using System;
using AlsTradingPost.Resources;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.UserServices.Models
{
    public class UpdateUserDdto : VersionedDdto
    {
        public Guid Id { get; set; }
        public string KnownAs { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public Persona Personas { get; set; }
    }
}
