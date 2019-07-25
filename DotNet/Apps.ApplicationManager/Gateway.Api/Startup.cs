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
using Common.Setup.Infrastructure.Messaging;
using Common.Setup.Infrastructure.Persistence;
using Common.Setup.Infrastructure.Settings;
using Common.Setup.Infrastructure.Startup;
using Common.Setup.Infrastructure.WebRequests;
using Gateway.Domain;
using Gateway.Setup;
using Gateway.Setup.Infrastructure.DomainEvents;
using Gateway.Setup.Infrastructure.Messaging;
using Gateway.Setup.Infrastructure.ReverseProxy;
using Vault.Broker;
using Vault.Broker.Setup.DataKeys;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[assembly: ApiController]

namespace Gateway.Api
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
                .UseWebRequests()
                .UseSystemClock()
                .AddLazyCache();

            services.Configure<MvcOptions>(options =>
            {
                options
                    .RequireHttps();
            });

            services
                .UseJsonV1Format()
                .RegisterMessageSubscribers()
                .RegisterDomainEventHandlers()
                .RegisterValidators()
                .RegisterApplicationServices()
                .RegisterDomainCommands()
                .RegisterPersistenceServices(Configuration)
                .RegisterProviders()
                .AddStartupTask<SetDataProtectorStartupTask>()
                .AddStartupTask<ApplyMigrationsStartupTask>()
                .AddStartupTask<InitialiseMessagingStartupTask>()
                .AddStartupTask<CreateDataKeysStartupTask<DataKeys>>()
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Map("/api", proxy =>
            {
                proxy
                    .UseExceptionMiddleware()
                    .UseReverseProxyMiddleware()
                    .Map("/v1", jsonVersion =>
                    {
                        jsonVersion
                            .UseJsonV1Middleware()
                            .UseHttpsRedirection()
                            .UseHsts()
                            .UseMvc();
                    });
            });
        }
    }
}