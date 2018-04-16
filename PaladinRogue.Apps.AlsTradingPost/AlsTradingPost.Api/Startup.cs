using System;
using AlsTradingPost.Setup;
using AlsTradingPost.Setup.Infrastructure.Settings;
using AutoMapper;
using Common.Api.Extensions;
using Common.Api.ResourceFormatter;
using Common.Api.Settings;
using Common.Application.Identity;
using Common.Domain.DomainEvents.Interfaces;
using Common.Resources.Concurrency.Interfaces;
using Common.Resources.Logging;
using Common.Setup.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Api
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<MvcOptions>(options =>
            {
                options.UseJsonOutputFormatter<CustomJsonOutputFormatter>(services)
                    .UseConcurrencyFilter()
                    .UseAppAccessAuthorizeFilter()
                    .RequireHttps();
            });
            
            services.AddScoped<ICurrentIdentityProvider, CurrentIdentityProvider>();

            services.Configure<ProxySettings>(Configuration.GetSection(nameof(ProxySettings)));
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.Configure<MessagingBusSettings>(Configuration.GetSection(nameof(MessagingBusSettings)));
            services.Configure<FacebookSettings>(Configuration.GetSection(nameof(FacebookSettings)));

            JwtRegistration.RegisterOptions(Configuration, services);
            EventRegistration.RegisterHandlers(services);

            MessageRegistration.RegisterSubscribers(services);
            ServiceRegistration.RegisterValidators(services);
            ServiceRegistration.RegisterApplicationServices( services);
            ServiceRegistration.RegisterDomainServices(services);
            ServiceRegistration.RegisterPersistenceServices(Configuration, services);

            ServiceRegistration.RegisterProviders(services);

            services.AddAutoMapper(MappingRegistration.RegisterMappers);

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IDomainEventHandlerFactory domainEventHandlerFactory)
        {
            domainEventHandlerFactory.Initialise();

            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            RewriteOptions options = new RewriteOptions()
                .AddRedirectToHttps();
            app.UseRewriter(options);

            MiddlewareRegistration.Register(app);
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
