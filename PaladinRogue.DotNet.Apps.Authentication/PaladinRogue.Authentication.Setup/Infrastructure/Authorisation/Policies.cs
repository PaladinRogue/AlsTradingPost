using PaladinRogue.Authentication.Application.Claims;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Authorisation
{
    public static class Policies
    {
        public const string User = JwtClaims.User;
    }
}