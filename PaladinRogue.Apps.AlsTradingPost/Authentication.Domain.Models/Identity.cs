using Common.Domain.Models;

namespace Authentication.Domain.Models
{
    public class Identity : Entity
    {
        public string AuthenticationId { get; set; }
    }
}
