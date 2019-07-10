using System;
using Common.Api.Extensions;
using Common.Setup.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReverseProxy.Setup;

namespace ReverseProxy.Api
{
    public class Startup : Common.Api.Startup
    {
        public Startup(IHostingEnvironment environment) : base(environment)
        {
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            CommonConfigureServices(services);

            services.Configure<MvcOptions>(options =>
            {
                options
                    .RequireHttps();
            });

            ServiceRegistration.RegisterServices(services);
            ServiceRegistration.RegisterPersistenceServices(Configuration, services);

            return services.BuildServiceProvider();
        }

        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory)
        {
            MiddlewareRegistration.Register(app);

            loggerFactory.AddLog4Net();

            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseMvc();
        }
    }
}