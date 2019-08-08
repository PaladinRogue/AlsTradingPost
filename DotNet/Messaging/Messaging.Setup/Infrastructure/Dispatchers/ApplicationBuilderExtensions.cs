using Microsoft.AspNetCore.Builder;

namespace Common.Setup.Infrastructure.Messaging
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDispatchMessagesMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<DispatchMessagesMiddleware>();
        }
    }
}