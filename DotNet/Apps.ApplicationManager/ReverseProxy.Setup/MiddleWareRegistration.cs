using Microsoft.AspNetCore.Builder;
using ReverseProxy.Setup.Infrastructure.ReverseProxy;

namespace ReverseProxy.Setup
{
    public class MiddlewareRegistration
    {
        public static void Register(IApplicationBuilder app)
        {
            app.UseMiddleware<ReverseProxyMiddleware>();
        }
    }
}