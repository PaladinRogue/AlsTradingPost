using AlsTradingPost.Setup.Middleware;
using Common.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AlsTradingPost.Setup
{
    public class MiddlewareRegistration
    {
        public static void RegisterTransactionPerRequest(IApplicationBuilder app)
        {
            app.UseMiddleware<TransactionPerRequestMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

