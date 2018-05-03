using System;

namespace Common.Authentication.Domain.SessionDomain.Models
{
    public class RefreshSessionProjection
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
    }
}