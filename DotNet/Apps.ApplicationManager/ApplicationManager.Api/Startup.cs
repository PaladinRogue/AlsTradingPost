using System;
using ApplicationManager.Setup;
using AutoMapper;
using Common.Api.Extensions;
using Common.Api.Formats;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.DataProtection;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Senders;
using Common.Setup;
using Common.Setup.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EventRegistration = ApplicationManager.Setup.EventRegistration;
using MessageRegistration = ApplicationManager.Setup.MessageRegistration;
using ServiceRegistration = ApplicationManager.Setup.ServiceRegistration;

[assembly: ApiController]
namespace ApplicationManager.Api
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
                    .UseConcurrencyFilter()
                    .UseBusinessExceptionFilter()
                    .UseValidationExceptionFilter()
                    .UseAppAccessAuthorizeFilter()
                    .RequireHttps();
            });

            SettingRegistration.RegisterSystemAdminIdentitySettings(Configuration, services);

            FormatRegistration.ConfigureJsonV1Format(services);

            JwtRegistration.RegisterOptions(Configuration, services);

            MessageRegistration.RegisterSubscribers(services);

            EventRegistration.RegisterHandlers(services);

            ServiceRegistration.RegisterBuilders(services);
            ServiceRegistration.RegisterValidators(services);
            ServiceRegistration.RegisterApplicationServices(services);
            ServiceRegistration.RegisterDomainServices(services);
            ServiceRegistration.RegisterPersistenceServices(Configuration, services);
            ServiceRegistration.RegisterProviders(services);
            ServiceRegistration.RegisterAuthorisation(services);
            ServiceRegistration.RegisterNotifications(services);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IDataProtector dataProtector,
            IDomainEventDispatcher domainEventDispatcher,
            IMessageSender messageSender)
        {
            DataProtection.SetDataProtector(dataProtector);
            DomainEvents.SetDomainEventDispatcher(domainEventDispatcher);
            Message.SetMessageSender(messageSender);

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net();

            MiddlewareRegistration.Register(app);

            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseMvc();
        }
    }
}