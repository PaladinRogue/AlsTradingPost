using System;
using AlsTradingPost.Setup;
using AutoMapper;
using Common.Api.Extensions;
using Common.Domain.DataProtection;
using Common.Domain.DomainEvents.Interfaces;
using Common.Messaging.Message.Interfaces;
using Common.Setup.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
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
                    .UseAppAccessAuthorizeFilter();

                if (!Environment.IsDevelopment())
                {
                    options.RequireHttps();
                }
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
            
            Common.Authentication.Setup.ServiceRegistration.RegisterDomainServices(services);
            Common.Authentication.Setup.ServiceRegistration.RegisterProviders(services);

            services.AddAutoMapper(MappingRegistration.RegisterMappers);

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IDomainEventHandlerFactory domainEventHandlerFactory,
            IMessageSubscriberFactory messageSubscriberFactory,
            IDataProtector dataProtector)
        {
            domainEventHandlerFactory.Initialise();
            messageSubscriberFactory.Initialise();
            
            DataProtection.SetDataProtector(dataProtector);

            loggerFactory.AddLog4Net();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                RewriteOptions options = new RewriteOptions()
                    .AddRedirectToHttps();
                app.UseRewriter(options);
            }

            MiddlewareRegistration.Register(app);
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
