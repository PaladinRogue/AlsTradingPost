using Microsoft.AspNetCore.Builder;

namespace ReverseProxy.Setup.Infrastructure.ReverseProxy
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseReverseProxyMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ReverseProxyMiddleware>();
        }
    }
}