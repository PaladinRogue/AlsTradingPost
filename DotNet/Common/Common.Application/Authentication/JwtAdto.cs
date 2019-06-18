using System;

namespace Common.Application.Authentication
{
    public class JwtAdto : IJwtAdto
    {
        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }

        public Guid SessionId { get; set; }
    }
}