using System;

namespace ApplicationManager.Domain.Identities.Login.RefreshToken
{
    public class RefreshTokenLoginCommandDdto
    {
        public Guid SessionId { get; set; }

        public string RefreshToken { get; set; }
    }
}