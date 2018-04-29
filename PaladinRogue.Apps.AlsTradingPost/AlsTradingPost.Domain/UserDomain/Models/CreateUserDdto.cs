using System;
using AlsTradingPost.Resources;

namespace AlsTradingPost.Domain.UserDomain.Models
{
    public class CreateUserDdto
    {
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
    }
}
