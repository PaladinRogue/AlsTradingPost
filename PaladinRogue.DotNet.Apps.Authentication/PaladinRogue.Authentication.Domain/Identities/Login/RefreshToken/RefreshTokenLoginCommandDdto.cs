using System;

namespace PaladinRogue.Authentication.Domain.Identities.Login.RefreshToken
{
    public class RefreshTokenLoginCommandDdto
    {
        public Guid? SessionId { get; set; }

        public string RefreshToken { get; set; }
    }
}