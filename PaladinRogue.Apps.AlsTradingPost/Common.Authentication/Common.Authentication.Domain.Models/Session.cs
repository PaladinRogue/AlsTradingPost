using Common.Domain.Models;

namespace Common.Authentication.Domain.Models
{
    public class Session : VersionedEntity
    {
        public string RefreshToken { get; set; }
        public bool Revoked { get; set; }
    }
}