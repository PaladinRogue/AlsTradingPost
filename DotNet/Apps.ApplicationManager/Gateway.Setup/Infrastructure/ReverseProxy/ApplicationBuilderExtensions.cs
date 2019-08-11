using Microsoft.AspNetCore.Builder;

namespace PaladinRogue.Gateway.Setup.Infrastructure.ReverseProxy
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseReverseProxyMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ReverseProxyMiddleware>();
        }
    }
}