using System;
using AlsTradingPost.Resources.Constants;
using Microsoft.AspNetCore.Http;

namespace AlsTradingPost.Resources.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Persona CurrentPersonas(this IHttpContextAccessor httpContextAccessor)
        {
            Enum.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimIdentifiers.Persona)?.Value, out Persona personas);

            return personas;
        }
    }
}
