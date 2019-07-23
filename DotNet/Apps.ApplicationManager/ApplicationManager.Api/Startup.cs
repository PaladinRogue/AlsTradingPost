using System;
using ApplicationManager.Setup;
using ApplicationManager.Setup.Infrastructure.Authorisation;
using ApplicationManager.Setup.Infrastructure.DomainEvents;
using ApplicationManager.Setup.Infrastructure.Messaging;
using AutoMapper;
using Common.Api.Builders;
using Common.Api.Extensions;
using Common.Api.Formats;
using Common.Domain.Clocks;
using Common.Domain.DataProtectors;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.Senders;
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
using NodaTime;
using Common.Setup.Infrastructure.DataProtection;
using Common.Setup.Infrastructure.Exceptions;
using Common.Setup.Infrastructure.WebRequests;
using KeyVault.Broker;
using KeyVault.Broker.Setup.DataKeys;

[assembly: ApiController]

namespace ApplicationManager.Api
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
                .LoadSystemAdminIdentitySettings(Configuration)
                .UseDefaultResourceBuilders()
                .UseSystemClock()
                .UseDomainEvents()
                .UseRabbitMqMessaging(Configuration)
                .RegisterCommonProviders()
                .RegisterCommonApplicationServices()
                .RegisterAuthorisationServices()
                .UseDataProtection()
                .UseKeyVault(Configuration)
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
                .UseJsonPolicyAuthorisation(Configuration)
                .UseEmailNotifications()
                .RegisterMessageSubscribers()
                .RegisterDomainEventHandlers()
                .RegisterValidators()
                .RegisterApplicationServices()
                .RegisterDomainCommands()
                .RegisterPersistenceServices(Configuration)
                .RegisterProviders()
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services.BuildServiceProvider();
        }

        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IDataProtector dataProtector,
            IDomainEventDispatcher domainEventDispatcher,
            IMessageSender messageSender,
            IDataHasher dataHasher,
            IClock clock)
        {
            dataProtector.SetDataProtector();
            dataHasher.SetDataHasher();
            domainEventDispatcher.SetDomainEventDispatcher();
            messageSender.SetMessageSender();
            clock.SetClock();

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