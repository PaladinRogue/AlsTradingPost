using Authentication.Setup;
using Authentication.Setup.Settings;
using AutoMapper;
using Common.Api.Extensions;
using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.DataProtection;
using Common.Messaging.Message.Interfaces;
using Common.Setup;
using Common.Setup.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MappingRegistration = Authentication.Api.Mappings.MappingRegistration;
using MessageRegistration = Authentication.Setup.MessageRegistration;
using ServiceRegistration = Authentication.Setup.ServiceRegistration;

namespace Authentication.Api
{
    public class Startup : Common.Api.Startup
    {
        public Startup(IHostingEnvironment environment) : base(environment)
        {
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            CommonConfigureServices(services);

            services.Configure<MvcOptions>(options =>
            {
                options.UseCamelCaseJsonOutputFormatter<JsonOutputFormatter>()
                    .UseValidationExceptionFilter()
                    .UseConcurrencyFilter()
                    .RequireHttps();
            });

            services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

            JwtRegistration.RegisterOptions(Configuration, services);
            
            MessageRegistration.RegisterSubscribers(services);
            
            ServiceRegistration.RegisterBuilders(services);
            ServiceRegistration.RegisterValidators(services);
            ServiceRegistration.RegisterApplicationServices(services);
            ServiceRegistration.RegisterDomainServices(services);
            ServiceRegistration.RegisterPersistenceServices(Configuration, services);
            ServiceRegistration.RegisterProviders(services);

            services.AddAutoMapper(MappingRegistration.RegisterMappers);
        }

        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IMessageSubscriberFactory messageSubscriberFactory,
            IDataProtector dataProtector,
            IDomainEventDispatcher domainEventDispatcher)
        {
            messageSubscriberFactory.Initialise();
            
            DataProtection.SetDataProtector(dataProtector);
            DomainEvents.SetDomainEventDispatcher(domainEventDispatcher);

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            loggerFactory.AddLog4Net();
            
            MiddlewareRegistration.Register(app);
            
            app.UseHsts();
            app.UseMvc();
        }
    }
}