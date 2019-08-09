using Common.Application.Authentication.Constants;

namespace Common.Api.Authorisation
{
    public class Policies
    {
        public const string AppAccess = JwtClaims.AppAccess;
        public const string RestrictedAppAccess = JwtClaims.RestrictedAppAccess;
    }
}
