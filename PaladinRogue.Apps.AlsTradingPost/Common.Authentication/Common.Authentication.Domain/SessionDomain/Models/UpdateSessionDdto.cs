using System;
using Common.Domain.Concurrency;

namespace Common.Authentication.Domain.SessionDomain.Models
{
    public class UpdateSessionDdto : VersionedDdto
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
        public bool Revoked { get; set; }
    }
}