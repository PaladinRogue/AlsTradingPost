using System;
using Common.Domain.Concurrency;

namespace Common.Authentication.Domain.SessionDomain.Models
{
    public class SessionProjection : VersionedProjection
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
        public bool IsRevoked { get; set; }
    }
}