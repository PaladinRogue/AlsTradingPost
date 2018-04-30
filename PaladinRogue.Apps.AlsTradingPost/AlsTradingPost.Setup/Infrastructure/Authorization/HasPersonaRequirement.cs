using AlsTradingPost.Resources;
using Microsoft.AspNetCore.Authorization;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public class HasPersonaRequirement : IAuthorizationRequirement
    {
        public PersonaFlags Persona { get; }

        public HasPersonaRequirement(PersonaFlags persona)
        {
            Persona = persona;
        }
    }
}