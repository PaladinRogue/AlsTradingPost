using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Common.Resources.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string CurrentIssuer(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor?.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Iss)?.Value;
        }
        
        public static Guid? CurrentSubject(this IHttpContextAccessor httpContextAccessor)
        {
            Guid.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid subjectId);

            return subjectId == Guid.Empty ? null : (Guid?)subjectId;
        }
        
        public static bool HasRequestHeader(this IHttpContextAccessor httpContextAccessor, string headerKey)
        {
            bool? containsKey = httpContextAccessor?.HttpContext?.Request?.Headers?.ContainsKey(headerKey);
            if (!containsKey.HasValue || !containsKey.Value) return false;
            
            bool.TryParse(httpContextAccessor.HttpContext.Request.Headers[headerKey], out bool headerValue);
                
            return headerValue;

        }
    }
}
