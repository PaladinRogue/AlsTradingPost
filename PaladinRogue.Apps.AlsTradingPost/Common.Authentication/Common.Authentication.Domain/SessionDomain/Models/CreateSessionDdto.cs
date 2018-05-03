using System;

namespace Common.Authentication.Domain.SessionDomain.Models
{
    public class CreateSessionDdto
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
        public bool Revoked { get; set; }
    }
}