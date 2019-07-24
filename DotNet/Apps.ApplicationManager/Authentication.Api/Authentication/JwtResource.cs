using System;
using Common.Api.Authentication;

namespace Authentication.Api.Authentication
{
    public class JwtResource : IJwtResource
    {
        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }

        public Guid SessionId { get; set; }
    }
}