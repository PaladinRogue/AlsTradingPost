using System;
using System.Collections.Generic;
using AlsTradingPost.Resources;
using AlsTradingPost.Resources.Constants;
using Microsoft.AspNetCore.Http;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public static class HttpContextAccessorExtensions
    {
        public static Persona CurrentPersonas(this IHttpContextAccessor httpContextAccessor)
        {
            Enum.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimIdentifiers.Persona)?.Value,
                out Persona personas);
            return personas;
        }

        public static IEnumerable<string> CurrentPersonaPolicies(this IHttpContextAccessor httpContextAccessor)
        {
            Enum.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimIdentifiers.Persona)?.Value,
                out Persona personas);

            return GetPolicies(personas);
        }

        private static IEnumerable<string> GetPolicies(Persona personas)
        {
            List<string> policies = new List<string>();
            foreach (Enum value in Enum.GetValues(personas.GetType()))
            {
                if (personas.HasFlag(value) && !value.Equals(Persona.None))
                {
                    policies.Add($"{PersonaPolicies.PREFIX}{value}");
                }
            }

            return policies;
        }
    }
}
