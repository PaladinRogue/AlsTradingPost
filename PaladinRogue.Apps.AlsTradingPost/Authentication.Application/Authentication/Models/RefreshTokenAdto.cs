using System;

namespace Authentication.Application.Authentication.Models
{
    public class RefreshTokenAdto
    {
        public Guid SessionId { get; set; }
        public string Token { get; set; }
    }
}