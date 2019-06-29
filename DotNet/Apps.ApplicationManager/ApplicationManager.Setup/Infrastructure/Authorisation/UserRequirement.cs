using Microsoft.AspNetCore.Authorization;

namespace ApplicationManager.Setup.Infrastructure.Authorisation
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public static bool IsUser => true;
    }
}