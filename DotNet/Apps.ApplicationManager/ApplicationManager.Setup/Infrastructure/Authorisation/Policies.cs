using ApplicationManager.ApplicationServices.Claims;

namespace ApplicationManager.Setup.Infrastructure.Authorisation
{
    public static class Policies
    {
        public const string User = JwtClaims.User;
    }
}