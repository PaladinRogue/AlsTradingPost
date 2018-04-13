using AlsTradingPost.Resources;
using Microsoft.AspNetCore.Authorization;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public class HasPersonaRequirement : IAuthorizationRequirement
    {
        public Persona Persona { get; }

        public HasPersonaRequirement(Persona persona)
        {
            Persona = persona;
        }
    }
}