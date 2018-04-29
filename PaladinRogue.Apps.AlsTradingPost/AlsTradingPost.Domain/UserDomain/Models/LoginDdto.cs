using System;

namespace AlsTradingPost.Domain.UserDomain.Models
{
    public class LoginDdto
    {
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
    }
}