using PaladinRogue.Libray.Core.Application.Authentication.Constants;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Authorisation
{
    public class Policies
    {
        public const string AppAccess = JwtClaims.AppAccess;
        public const string RestrictedAppAccess = JwtClaims.RestrictedAppAccess;
    }
}
