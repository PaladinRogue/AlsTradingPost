using Microsoft.AspNetCore.Authorization;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Authorisation
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public static bool IsUser => true;
    }
}