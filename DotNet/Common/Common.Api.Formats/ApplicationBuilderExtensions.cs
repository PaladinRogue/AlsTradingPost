using Common.Api.Formats.JsonV1;
using Microsoft.AspNetCore.Builder;

namespace Common.Api.Formats
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseJsonV1Middleware(this IApplicationBuilder apps)
        {
            return apps.UseMiddleware<JsonV1Middleware>();
        }
    }
}