using System;

namespace Common.Authentication.Domain.SessionDomain.Models
{
    public class RefreshSessionDdto
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
    }
}