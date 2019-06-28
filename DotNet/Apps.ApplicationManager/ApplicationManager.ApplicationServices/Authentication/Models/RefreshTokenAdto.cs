using System;

namespace ApplicationManager.ApplicationServices.Authentication.Models
{
    public class RefreshTokenAdto
    {
        public Guid SessionId { get; set; }

        public string Token { get; set; }
    }
}