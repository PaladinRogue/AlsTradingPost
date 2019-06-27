using Common.Setup.Infrastructure.Exceptions;
using Common.Setup.Infrastructure.Messaging;
using Microsoft.AspNetCore.Builder;

namespace Common.Setup
{
    public class MiddlewareRegistration
    {
        public static void Register(IApplicationBuilder app)
        {
            app.UseMiddleware<DispatchMessagesMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

