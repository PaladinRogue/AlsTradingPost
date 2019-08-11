using Microsoft.AspNetCore.Builder;
using PaladinRogue.Libray.Core.Api.Formats.JsonV1;

namespace PaladinRogue.Libray.Core.Api.Formats
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseJsonV1Middleware(this IApplicationBuilder apps)
        {
            return apps.UseMiddleware<JsonV1Middleware>();
        }
    }
}