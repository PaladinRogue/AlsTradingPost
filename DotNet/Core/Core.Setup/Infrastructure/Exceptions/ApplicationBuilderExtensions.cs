using Microsoft.AspNetCore.Builder;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Exceptions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}