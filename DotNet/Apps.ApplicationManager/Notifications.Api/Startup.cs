using System;
using AutoMapper;
using Common.Api.Builders;
using Common.Api.DataProtection;
using Common.Api.Extensions;
using Common.Api.Formats;
using Common.Authorisation.Policies;
using Common.Setup;
using Common.Setup.Infrastructure.DataProtection;
using Common.Setup.Infrastructure.DomainEvents;
using Common.Setup.Infrastructure.Exceptions;
using Common.Setup.Infrastructure.Logging;
using Common.Setup.Infrastructure.Messaging;
using Common.Setup.Infrastructure.Settings;
using Common.Setup.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notifications.Setup;
using Vault.Broker;
using Vault.Broker.Setup.DataKeys;

[assembly: ApiController]

namespace Notifications.Api
{
    public class Startup : VaultApiStartup
    {
        public Startup(IHostingEnvironment environment) : base(environment)
        {
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddDefaultMvcOptions()
                .LoadAppSettings(Configuration)
                .LoadHostSettings(Configuration)
                .LoadMasterKey(Configuration)
                .UseDomainEvents()
                .UseRabbitMqMessaging(Configuration)
                .UseDataProtection()
                .UseVault(Configuration)
                .UseDefaultResourceBuilders()
                .RegisterAuthorisationServices()
                .UseAlwaysAllowAuthorisation()
                .RegisterCommonProviders()
                .UseSystemClock()
                .AddLazyCache();

            services.Configure<MvcOptions>(options =>
            {
                options
                    .RequireHttps();
            });

            services
                .UseJsonV1Format()
                .RegisterApplicationServices()
                .RegisterProviders()
                .AddStartupTask<SetDataProtectorStartupTask>()
                .AddStartupTask<InitialiseMessagingStartupTask>()
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services.BuildServiceProvider();
        }

        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            app.Map("/v1", jsonVersion =>
            {
                jsonVersion
                    .UseDispatchMessagesMiddleware()
                    .UseExceptionMiddleware()
                    .UseJsonV1Middleware()
                    .UseHttpsRedirection()
                    .UseHsts()
                    .UseMvc();
            });
        }
    }
}