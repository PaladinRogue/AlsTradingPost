using System;
using AlsTradingPost.Setup;
using AutoMapper;
using Common.Api.Extensions;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.DataProtection;
using Common.Messaging.Message.Interfaces;
using Common.Setup.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Api
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
                options.UseCamelCaseJsonOutputFormatter<JsonOutputFormatter>()
                    .UseConcurrencyFilter()
                    .UseValidationExceptionFilter()
                    .UseAppAccessAuthorizeFilter()
                    .RequireHttps();
            });
            
            JwtRegistration.RegisterOptions(Configuration, services);
            
            EventRegistration.RegisterHandlers(services);

            MessageRegistration.RegisterSubscribers(services);
            
            ServiceRegistration.RegisterValidators(services);
            ServiceRegistration.RegisterBuilders(services);
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
            IMessageSubscriberFactory messageSubscriberFactory,
            IDomainEventDispatcher domainEventDispatcher,
            IDataProtector dataProtector,
            IServiceProvider serviceProvider)
        {
            messageSubscriberFactory.Initialise();
            
            DataProtection.SetDataProtector(dataProtector);
            DomainEvents.SetDomainEventDispatcher(domainEventDispatcher);

            loggerFactory.AddLog4Net();
            
            app.UseHttpsRedirection();

            Common.Setup.MiddlewareRegistration.Register(app);

            app.UseHsts();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
