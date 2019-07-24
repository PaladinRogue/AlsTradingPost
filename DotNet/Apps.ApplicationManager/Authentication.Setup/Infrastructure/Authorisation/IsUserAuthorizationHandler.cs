using System.Threading.Tasks;
using Authentication.ApplicationServices.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.Setup.Infrastructure.Authorisation
{
    public class IsUserAuthorizationHandler : AuthorizationHandler<UserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == JwtClaimIdentifiers.User && UserRequirement.IsUser))
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            context.Fail();

            return Task.CompletedTask;
        }
    }
}