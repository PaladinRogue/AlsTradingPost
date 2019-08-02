using Authentication.ApplicationServices.Claims;

namespace Authentication.Setup.Infrastructure.Authorisation
{
    public static class Policies
    {
        public const string User = JwtClaims.User;
    }
}