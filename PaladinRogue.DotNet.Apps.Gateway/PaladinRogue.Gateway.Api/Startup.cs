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
using PaladinRogue.Library.Authorisation.Setup;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies;
using PaladinRogue.Library.Core.Api;
using PaladinRogue.Library.Core.Api.Builders;
using PaladinRogue.Library.Core.Api.DataProtection;
using PaladinRogue.Library.Core.Api.Extensions;
using PaladinRogue.Library.Core.Api.Formats;
using PaladinRogue.Library.Core.Setup;
using PaladinRogue.Library.Core.Setup.Infrastructure.DataProtection;
using PaladinRogue.Library.Core.Setup.Infrastructure.Exceptions;
using PaladinRogue.Library.Core.Setup.Infrastructure.Persistence;
using PaladinRogue.Library.Core.Setup.Infrastructure.Settings;
using PaladinRogue.Library.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Library.Core.Setup.Infrastructure.WebRequests;
using PaladinRogue.Library.DomainEvents.Setup;
using PaladinRogue.Library.Messaging.Setup;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Startup;
using PaladinRogue.Library.Vault.Setup;
using PaladinRogue.Library.Vault.Setup.Infrastructure.Startup;

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