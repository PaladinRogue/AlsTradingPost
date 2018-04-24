using System;
using System.Collections.Generic;
using AlsTradingPost.Resources;
using AlsTradingPost.Resources.Constants;
using Microsoft.AspNetCore.Http;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public static class HttpContextAccessorExtensions
    {
        public static Persona CurrentPersona(this IHttpContextAccessor httpContextAccessor)
        {
            Enum.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimIdentifiers.Persona)?.Value,
                out Persona persona);
            
            return persona;
        }
    }
}
