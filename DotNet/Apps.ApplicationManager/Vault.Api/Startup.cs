using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Policies;
using PaladinRogue.Libray.Core.Api;
using PaladinRogue.Libray.Core.Api.Builders;
using PaladinRogue.Libray.Core.Api.Clocks;
using PaladinRogue.Libray.Core.Api.DataProtection;
using PaladinRogue.Libray.Core.Api.DomainEvents;
using PaladinRogue.Libray.Core.Api.Extensions;
using PaladinRogue.Libray.Core.Api.Formats;
using PaladinRogue.Libray.Core.Setup;
using PaladinRogue.Libray.Core.Setup.Infrastructure.DataProtection;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Exceptions;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Logging;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Persistence;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Settings;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Libray.Core.Setup.Infrastructure.WebRequests;
using PaladinRogue.Libray.DomainEvents.Setup;
using PaladinRogue.Libray.Messaging.Setup;
using PaladinRogue.Libray.Messaging.Setup.Infrastructure.Dispatchers;
using PaladinRogue.Libray.Messaging.Setup.Infrastructure.Startup;
using PaladinRogue.Libray.Vault.Setup;
using PaladinRogue.Vault.Setup;
using PaladinRogue.Vault.Setup.Infrastructure.DataKeys;

[assembly: ApiController]

namespace PaladinRogue.Vault.Api
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
                .UseDefaultResourceBuilders()
                .UseSystemClock()
                .UseDomainEvents()
                .UseRabbitMqMessaging(Configuration)
                .UseDataProtection()
                .UseWebRequests()
                .UseFluentValidation()
                .AddCommonProviders()
                .AddLazyCache();

            services.Configure<MvcOptions>(options =>
            {
                options
                    .UseConcurrencyFilter()
                    .UseBusinessExceptionFilter()
                    .UseValidationExceptionFilter()
                    .RequireHttps();
            });

            services
                .UseJsonV1Format()
                .UseAlwaysDenyAuthorisation()
                .UseDefaultRouting()
                .AddVaultPersistence(Configuration)
                .AddApplicationDomain()
                .AddSharedDataKeyDomain()
                .AddDataKeyProviders()
                .LoadMasterKey(Configuration)
                .AddStartupTask<SetDataProtectorStartupTask>()
                .AddStartupTask<SetDomainEventDispatcherStartupTask>()
                .AddStartupTask<SetMessageSenderStartupTask>()
                .AddStartupTask<SetClockStartupTask>()
                .AddStartupTask<ApplyMigrationsStartupTask>()
                .AddStartupTask<InitialiseMessagingStartupTask>()
                .AddStartupTask<CreateSharedDataKeysStartupTask>()
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