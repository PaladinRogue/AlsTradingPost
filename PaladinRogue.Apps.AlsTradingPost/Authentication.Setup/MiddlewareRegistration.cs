﻿using Common.Api.Middleware;
using Common.Setup.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Authentication.Setup
{
    public class MiddlewareRegistration
    {
        public static void Register(IApplicationBuilder app)
        {
            app.UseMiddleware<TransactionPerRequestMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

