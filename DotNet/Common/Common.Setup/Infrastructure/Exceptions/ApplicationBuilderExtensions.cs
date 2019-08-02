using Microsoft.AspNetCore.Builder;

namespace Common.Setup.Infrastructure.Exceptions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}