using System;
using Authentication.Domain;
using Authentication.Setup;
using Authentication.Setup.Infrastructure.Authorisation;
using Authentication.Setup.Infrastructure.DomainEvents;
using Authentication.Setup.Infrastructure.Messaging;
using Authentication.Setup.Infrastructure.SystemAdmins;
using Authorisation.Setup;
using AutoMapper;
using Common.Api;
using Common.Api.ApplicationRegistration;
using Common.Api.Builders;
using Common.Api.Clocks;
using Common.Api.DataProtection;
using Common.Api.DomainEvents;
using Common.Api.Extensions;
using Common.Api.Formats;
using Common.Setup;
using Common.Setup.Infrastructure.DomainEvents;
using Common.Setup.Infrastructure.Logging;
using Common.Setup.Infrastructure.Messaging;
using Common.Setup.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Common.Setup.Infrastructure.DataProtection;
using Common.Setup.Infrastructure.Exceptions;
using Common.Setup.Infrastructure.Persistence;
using Common.Setup.Infrastructure.Startup;
using Common.Setup.Infrastructure.WebRequests;
using Libs.Vault.Broker;
using Messaging.Setup.Infrastructure.Startup;
using Vault.Broker;
using Vault.Broker.Setup.DataKeys;

[assembly: ApiController]

namespace Authentication.Api
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
                .LoadSystemAdminIdentitySettings(Configuration)
                .UseDefaultResourceBuilders()
                .UseSystemClock()
                .UseDomainEvents()
                .UseRabbitMqMessaging(Configuration)
                .UseFluentValidation()
                .AddCommonProviders()
                .RegisterCommonApplicationServices()
                .RegisterAuthorisationServices()
                .UseDataProtection()
                .UseVault(Configuration)
                .UseWebRequests()
                .AddLazyCache();

            services.Configure<MvcOptions>(options =>
            {
                options
                    .UseConcurrencyFilter()
                    .UseBusinessExceptionFilter()
                    .UseValidationExceptionFilter()
                    .UseAppAccessAuthorizeFilter()
                    .RequireHttps();
            });

            services
                .UseJsonV1Format()
                .UseEmailNotifications()
                .RegisterMessageHandlers()
                .RegisterDomainEventSubscribers()
                .AddAuthenticationServiceDomain()
                .AddUserDomain()
                .AddNotificationDomain()
                .AddIdentityDomain()
                .AddAuthenticationPersistence(Configuration)
                .UseDefaultRouting()
                .UseJsonPolicyAuthorisation(Configuration)
                .AddStartupTask<SetDataProtectorStartupTask>()
                .AddStartupTask<SetDataHasherStartupTask>()
                .AddStartupTask<SetDomainEventDispatcherStartupTask>()
                .AddStartupTask<SetMessageSenderStartupTask>()
                .AddStartupTask<SetClockStartupTask>()
                .AddStartupTask<ApplyMigrationsStartupTask>()
                .AddStartupTask<InitialiseMessagingStartupTask>()
                .AddStartupTask<CreateDataKeysStartupTask<DataKeys>>()
                .AddStartupTask<RegisterApplicationStartupTask>()
                .AddStartupTask<RegisterSystemAdminStartupTask>()
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
                    .UseAuthentication()
                    .UseHttpsRedirection()
                    .UseHsts()
                    .UseMvc();
            });
        }
    }
}