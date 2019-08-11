using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Gateway.Domain;
using PaladinRogue.Gateway.Setup;
using PaladinRogue.Gateway.Setup.Infrastructure.DomainEvents;
using PaladinRogue.Gateway.Setup.Infrastructure.Messaging;
using PaladinRogue.Gateway.Setup.Infrastructure.ReverseProxy;
using PaladinRogue.Libray.Authorisation.Setup;
using PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Policies;
using PaladinRogue.Libray.Core.Api;
using PaladinRogue.Libray.Core.Api.Builders;
using PaladinRogue.Libray.Core.Api.DataProtection;
using PaladinRogue.Libray.Core.Api.Extensions;
using PaladinRogue.Libray.Core.Api.Formats;
using PaladinRogue.Libray.Core.Setup;
using PaladinRogue.Libray.Core.Setup.Infrastructure.DataProtection;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Exceptions;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Persistence;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Settings;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Libray.Core.Setup.Infrastructure.WebRequests;
using PaladinRogue.Libray.DomainEvents.Setup;
using PaladinRogue.Libray.Messaging.Setup;
using PaladinRogue.Libray.Messaging.Setup.Infrastructure.Startup;
using PaladinRogue.Libray.Vault.Setup;
using PaladinRogue.Libray.Vault.Setup.Infrastructure.Startup;

[assembly: ApiController]

namespace PaladinRogue.Gateway.Api
{
    public class Startup : ApiStartup
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
                .AddCommonProviders()
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
                .RegisterMessageHandlers()
                .RegisterDomainEventSubscribers()
                .AddApplicationDomain()
                .AddGatewayPersistence(Configuration)
                .UseGatewayRouteProvider()
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