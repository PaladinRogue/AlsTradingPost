using Microsoft.AspNetCore.Authorization;

namespace ApplicationManager.Setup.Infrastructure.Authorisation
{
    public class IsUserRequirement : IAuthorizationRequirement
    {
        public static bool IsUser => true;
    }
}