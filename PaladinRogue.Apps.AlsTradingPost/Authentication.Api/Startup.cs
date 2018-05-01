using System;
using Authentication.Setup;
using Authentication.Setup.Settings;
using AutoMapper;
using Common.Api.Extensions;
using Common.Domain.DomainEvents.Interfaces;
using Common.Messaging.Message.Interfaces;
using Common.Setup.Infrastructure.Exceptions;
using Common.Setup.Infrastructure.Logging;
using Common.Setup.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            CommonConfigureServices(services);

            services.Configure<MvcOptions>(options =>
            {
                options.UseCamelCaseJsonOutputFormatter<JsonOutputFormatter>()
                    .UseConcurrencyFilter();

                if (!Environment.IsDevelopment())
                {
                    options.RequireHttps();
                }
            });

            services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

            JwtRegistration.RegisterOptions(Configuration, services);
            
            MessageRegistration.RegisterSubscribers(services);
            
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
            IMessageSubscriberFactory messageSubscriberFactory)
        {
            domainEventHandlerFactory.Initialise();
            messageSubscriberFactory.Initialise();

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
            
            loggerFactory.AddLog4Net();
            
            app.UseMiddleware<TransactionPerRequestMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMvc();
        }
    }
}