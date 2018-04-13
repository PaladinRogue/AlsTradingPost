using System;
using Authentication.Setup;
using Authentication.Setup.Settings;
using AutoMapper;
using Common.Api.Extensions;
using Common.Api.Settings;
using Common.Domain.DomainEvents.Interfaces;
using Common.Messaging.Message.Interfaces;
using Common.Resources.Logging;
using Common.Setup.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MappingRegistration = Authentication.Api.Mappings.MappingRegistration;

namespace Authentication.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<MvcOptions>(options =>
            {
                options.UseCustomJsonOutputFormatter()
                    .UseConcurrencyFilter()
                    .RequireHttps();
            });

            services.Configure<ProxySettings>(Configuration.GetSection(nameof(ProxySettings)));
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));
            services.Configure<MessagingBusSettings>(Configuration.GetSection(nameof(MessagingBusSettings)));

            JwtRegistration.RegisterOptions(Configuration, services);
            EventRegistration.RegisterHandlers(services);
            MessageRegistration.RegisterSubscribers(services);
            ServiceRegistration.RegisterServices(Configuration, services);
            ServiceRegistration.RegisterProviders(services);

            services.AddAutoMapper(MappingRegistration.RegisterMappers);

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory,
            IDomainEventHandlerFactory domainEventHandlerFactory,
            IMessageSubscriberFactory messageSubscriberFactory)
        {
            domainEventHandlerFactory.Initialise();
            messageSubscriberFactory.Initialise();
            
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new RewriteOptions()
                .AddRedirectToHttps();
            app.UseRewriter(options);

            MiddlewareRegistration.Register(app);

            app.UseMvc();
        }
    }
}