using System;
using AlsTradingPost.Application.Claims;
using AlsTradingPost.Resources;
using Microsoft.AspNetCore.Http;

namespace AlsTradingPost.Setup.Infrastructure.Authorisation
{
    public static class HttpContextAccessorExtensions
    {
        public static PersonaFlags CurrentPersonaFlags(this IHttpContextAccessor httpContextAccessor)
        {
            Enum.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimIdentifiers.Persona)?.Value,
                out PersonaFlags persona);
            
            return persona;
        }

        public static Guid? CurrentTrader(this IHttpContextAccessor httpContextAccessor)
        {
            Guid.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimIdentifiers.Trader)?.Value, out Guid traderId);

            return traderId == Guid.Empty ? null : (Guid?)traderId;
        }

        public static Guid? CurrentAdmin(this IHttpContextAccessor httpContextAccessor)
        {
            Guid.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(JwtClaimIdentifiers.Admin)?.Value, out Guid adminId);

            return adminId == Guid.Empty ? null : (Guid?)adminId;
        }
    }
}
