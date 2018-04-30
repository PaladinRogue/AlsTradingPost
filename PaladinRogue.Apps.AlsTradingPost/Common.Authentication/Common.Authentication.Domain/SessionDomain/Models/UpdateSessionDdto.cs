using System;

namespace Common.Authentication.Domain.SessionDomain.Models
{
    public class UpdateSessionDdto
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
        public bool Revoked { get; set; }
    }
}