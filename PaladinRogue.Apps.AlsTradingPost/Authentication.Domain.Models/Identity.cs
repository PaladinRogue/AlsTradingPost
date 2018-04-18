using Common.Domain.Models;

namespace Authentication.Domain.Models
{
    public class Identity : VersionedEntity
    {
        public string AuthenticationId { get; set; }
    }
}
