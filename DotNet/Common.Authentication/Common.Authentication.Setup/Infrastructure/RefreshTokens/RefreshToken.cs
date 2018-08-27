using System;

namespace Common.Authentication.Setup.Infrastructure.RefreshTokens
{
    public class RefreshToken
    {
        public Guid Randomizer { get; }

        private RefreshToken()
        {
            Randomizer = Guid.NewGuid();
        }

        public static RefreshToken Create()
        {
            return new RefreshToken();
        }
    }
}