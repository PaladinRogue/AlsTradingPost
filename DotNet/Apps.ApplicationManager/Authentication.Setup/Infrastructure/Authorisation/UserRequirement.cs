using Microsoft.AspNetCore.Authorization;

namespace Authentication.Setup.Infrastructure.Authorisation
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public static bool IsUser => true;
    }
}