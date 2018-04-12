using System;
using AlsTradingPost.Resources;

namespace AlsTradingPost.Domain.UserServices.Models
{
    public class CreateUserDdto
    {
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public Persona Personas { get; set; }
    }
}
