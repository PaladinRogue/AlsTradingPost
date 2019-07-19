using System;
using AutoMapper;
using Common.Api.Builders;
using Common.Api.Extensions;
using Common.Api.Formats;
using Common.Authorisation.Policies;
using Common.Domain.Clocks;
using Common.Domain.DataProtectors;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.Senders;
using Common.Setup;
using Common.Setup.Infrastructure.DataProtection;
using Common.Setup.Infrastructure.DomainEvents;
using Common.Setup.Infrastructure.Exceptions;
using Common.Setup.Infrastructure.Logging;
using Common.Setup.Infrastructure.Messaging;
using Common.Setup.Infrastructure.Settings;
using Common.Setup.Infrastructure.WebRequests;
using KeyVault.Setup;
using KeyVault.Setup.Infrastructure.DomainEvents;
using KeyVault.Setup.Infrastructure.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NodaTime;

[assembly: ApiController]

namespace KeyVault.Api
{
    public class Startup : Common.Api.Startup
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
                .UseDataProtection(Configuration)
                .RegisterCommonProviders()
                .UseWebRequests()
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
                .RegisterMessageSubscribers()
                .RegisterDomainEventHandlers()
                .RegisterValidators()
                .RegisterApplicationServices()
                .RegisterDomainCommands()
                .RegisterProviders()
                .RegisterPersistenceServices(Configuration)
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services.BuildServiceProvider();
        }

        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IDataProtector dataProtector,
            IDomainEventDispatcher domainEventDispatcher,
            IMessageSender messageSender,
            IClock clock)
        {
            dataProtector.SetDataProtector();
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
                    .UseHttpsRedirection()
                    .UseHsts()
                    .UseMvc();
            });
        }
    }
}