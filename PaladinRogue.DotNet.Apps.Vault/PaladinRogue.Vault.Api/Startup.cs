using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies;
using PaladinRogue.Library.Core.Api;
using PaladinRogue.Library.Core.Api.Builders;
using PaladinRogue.Library.Core.Api.Clocks;
using PaladinRogue.Library.Core.Api.DataProtection;
using PaladinRogue.Library.Core.Api.DomainEvents;
using PaladinRogue.Library.Core.Api.Extensions;
using PaladinRogue.Library.Core.Api.Formats;
using PaladinRogue.Library.Core.Setup;
using PaladinRogue.Library.Core.Setup.Infrastructure.DataProtection;
using PaladinRogue.Library.Core.Setup.Infrastructure.Exceptions;
using PaladinRogue.Library.Core.Setup.Infrastructure.Logging;
using PaladinRogue.Library.Core.Setup.Infrastructure.Persistence;
using PaladinRogue.Library.Core.Setup.Infrastructure.Settings;
using PaladinRogue.Library.Core.Setup.Infrastructure.Startup;
using PaladinRogue.Library.Core.Setup.Infrastructure.WebRequests;
using PaladinRogue.Library.DomainEvents.Setup;
using PaladinRogue.Library.Messaging.Setup;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Dispatchers;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Startup;
using PaladinRogue.Library.Vault.Setup;
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

            loggerFactory.AddLogging();

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