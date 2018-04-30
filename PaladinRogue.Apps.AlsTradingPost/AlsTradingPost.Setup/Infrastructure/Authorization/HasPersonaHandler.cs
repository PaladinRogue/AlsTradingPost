using System;
using System.Threading.Tasks;
using AlsTradingPost.Application.Claims;
using AlsTradingPost.Resources;
using Microsoft.AspNetCore.Authorization;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public class HasPersonaHandler : AuthorizationHandler<HasPersonaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPersonaRequirement requirement)
        {
            if (context.User.HasClaim(c =>
            {
                Enum.TryParse(c.Value, out PersonaFlags persona);
                return c.Type == JwtClaimIdentifiers.Persona &&
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
