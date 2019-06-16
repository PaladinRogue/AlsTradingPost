using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ApplicationManager.Setup.Infrastructure.Authorisation
{
    public class IsUserAuthorizationHandler : AuthorizationHandler<IsUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsUserRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == JwtClaimIdentifiers.User &&
                                           IsUserRequirement.IsUser))
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }
            context.Fail();

            return Task.CompletedTask;
        }
    }
}