using System;

namespace Common.Authentication.Domain.SessionDomain.Models
{
    public class CreateSessionProjection
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
    }
}