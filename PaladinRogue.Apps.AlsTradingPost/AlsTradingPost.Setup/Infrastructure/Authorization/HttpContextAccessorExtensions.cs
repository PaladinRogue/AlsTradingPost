using System;
using AlsTradingPost.Application.Claims;
using AlsTradingPost.Resources;
using Microsoft.AspNetCore.Http;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public static class HttpContextAccessorExtensions
    {
        public static PersonaFlags CurrentPersona(this IHttpContextAccessor httpContextAccessor)
        {
            Enum.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimIdentifiers.Persona)?.Value,
                out PersonaFlags persona);
            
            return persona;
        }
    }
}
