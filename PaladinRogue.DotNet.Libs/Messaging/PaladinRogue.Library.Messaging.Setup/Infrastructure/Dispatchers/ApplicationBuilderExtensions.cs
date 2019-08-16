using Microsoft.AspNetCore.Builder;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Dispatchers
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDispatchMessagesMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<DispatchMessagesMiddleware>();
        }
    }
}