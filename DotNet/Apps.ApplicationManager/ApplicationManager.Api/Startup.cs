using System;
using ApplicationManager.Setup;
using AutoMapper;
using Common.Api.Extensions;
using Common.Api.Formats;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.DataProtection;
using Common.Messaging.Message.Interfaces;
using Common.Setup;
using Common.Setup.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MappingRegistration = ApplicationManager.Api.Mappings.MappingRegistration;
using MessageRegistration = ApplicationManager.Setup.MessageRegistration;
using ServiceRegistration = ApplicationManager.Setup.ServiceRegistration;

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
                    .RequireHttps();
            });

            FormatRegistration.ConfigureJsonV1Format(services);
            
            JwtRegistration.RegisterOptions(Configuration, services);
            
            MessageRegistration.RegisterSubscribers(services);
            
            ServiceRegistration.RegisterBuilders(services);
            ServiceRegistration.RegisterValidators(services);
            ServiceRegistration.RegisterApplicationServices(services);
            ServiceRegistration.RegisterDomainServices(services);
            ServiceRegistration.RegisterPersistenceServices(Configuration, services);
            ServiceRegistration.RegisterProviders(services);
            ServiceRegistration.RegisterAuthorisation(services);

            services.AddAutoMapper(MappingRegistration.RegisterMappers);

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IDataProtector dataProtector,
            IDomainEventDispatcher domainEventDispatcher, 
            IMessageBus messageBus)
        {
            DataProtection.SetDataProtector(dataProtector);
            DomainEvents.SetDomainEventDispatcher(domainEventDispatcher);

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