using System;
using Common.Api.Authentication.Interfaces;

namespace ApplicationManager.Api.Authentication
{
    public class JwtResource : IJwtResource
    {
        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }

        public Guid SessionId { get; set; }
    }
}