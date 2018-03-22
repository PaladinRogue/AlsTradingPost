using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class PersonalDetails : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
    }
}
