using Microsoft.AspNetCore.Builder;

namespace PaladinRogue.Libray.Messaging.Setup.Infrastructure.Dispatchers
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDispatchMessagesMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<DispatchMessagesMiddleware>();
        }
    }
}