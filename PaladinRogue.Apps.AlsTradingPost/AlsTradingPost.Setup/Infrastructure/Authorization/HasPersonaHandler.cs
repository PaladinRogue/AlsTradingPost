using System;
using System.Threading.Tasks;
using AlsTradingPost.Resources;
using Microsoft.AspNetCore.Authorization;
using JwtClaims = AlsTradingPost.Resources.Constants.JwtClaims;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public class HasPersonaHandler : AuthorizationHandler<HasPersonaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPersonaRequirement requirement)
        {
            if (context.User.HasClaim(c =>
            {
                Enum.TryParse(c.Value, out Persona persona);
                return c.Type == JwtClaims.Persona &&
                       persona == requirement.Persona;
            }))
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }
            context.Fail();

            return Task.CompletedTask;
        }
    }
}
